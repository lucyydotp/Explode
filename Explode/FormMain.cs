using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Threading;
using ExplodePluginBase;
using System.Diagnostics;

namespace Explode {
    public partial class FormMain : Form
    {


        // Creates a new plugin manager system and loads plugins
        public PluginManager manager;
        private string directory;
        private Thread execThread;

        //history stuff
        public List<string> pathHistory = new List<string>();
        int historyPointer = -1;
        bool updateHistory = true;

        private string _labelOld;

        public FormMain()
        {
            InitializeComponent();
            manager = new PluginManager(Directory.GetCurrentDirectory(), lstFiles);

        }

        // this properly controls directory changes and input validation
        public string CurrentDirectory
        {
            get { return directory; }
            set
            {
                // makes sure it's not already being updated
                if (GlobalVars.isUpdating == false)
                {
                    // shows that an update is in process
                    GlobalVars.isUpdating = true;

                    // makes sure the new folder actually exists
                    if (Directory.Exists(value))
                    {
                        //verify the user has sufficient access to the directory
                        //this will trigger on certain special pointer directories (ie. legacy symlinks like Application Data)
                        try
                        {
                            Directory.GetFileSystemEntries(value);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("You don't have permission to access this directory.",
                                "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // if it doesn't end with a /, add one
                        if (value.Replace("\\", "/").EndsWith("/") == false)
                        {
                            directory = value.Replace("\\", "/") + "/";
                        }
                        else
                        {
                            directory = value.Replace("\\", "/");
                        }

                        execThread = new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true;
                            FileUpdateHandler.UpdateUI(this);
                        });
                        execThread.Start();

                        if (updateHistory)
                        {
                            historyPointer++;
                            if (historyPointer <= pathHistory.Count() - 1)
                                pathHistory.RemoveRange(historyPointer, pathHistory.Count() - historyPointer);
                            pathHistory.Add(directory);

                        }

                        if (historyPointer == 0) btnBack.Enabled = false;
                        else btnBack.Enabled = true;

                        if (historyPointer == pathHistory.Count() - 1) btnForward.Enabled = false;
                        else btnForward.Enabled = true;
                    }
                }
            }
        }

        #region Form Events

        private void FormMain_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                FileUpdateHandler.UpdateQuickAccess(this);
            }).Start();
            CurrentDirectory = "C:/Users";
        }

        private void txtCurrentDirectory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                CurrentDirectory = txtCurrentDirectory.Text;
            }
        }

        private void lstFiles_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = lstFiles.SelectedItems[0];
            IOFunctions.OpenEntry(this, selectedItem);
        }

        // called when back button pressed
        private void GoBack(object sender, EventArgs e)
        {
            if (GlobalVars.isUpdating == false)
            {
                updateHistory = false;
                historyPointer--;
                CurrentDirectory = pathHistory[historyPointer];
                updateHistory = true;
            }
        }


        private void GoForward(object sender, EventArgs e)
        {
            if (GlobalVars.isUpdating == false)
            {
                updateHistory = false;
                historyPointer++;
                CurrentDirectory = pathHistory[historyPointer];
                updateHistory = true;
            }
        }

        private void Refresh(object sender, EventArgs e)
        {
            if (GlobalVars.isUpdating == false)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    FileUpdateHandler.UpdateUI(this);
                }).Start();
            }
        }

        private void btnGoUp_Click(object sender, EventArgs e)
        {
            if (GlobalVars.isUpdating == false)
            {
                try
                {
                    CurrentDirectory = new DirectoryInfo(CurrentDirectory).Parent.FullName;
                }
                catch (NullReferenceException)
                {
                    // probably means that we're at the root dir, so we can do nothing
                    ;
                }
            }
        }

        private void lstFiles_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (lstFiles.FocusedItem != null && lstFiles.FocusedItem.Bounds.Contains(e.Location) == true) {
                    if (lstFiles.SelectedItems.Count == 1) {
                        ctxtSingleRightClick.Show(Cursor.Position);
                    } else if (lstFiles.SelectedItems.Count > 1) {
                        ctxtMultiRightClick.Show(Cursor.Position);
                    }
                } else {
                    if (Clipboard.ContainsFileDropList()) cmiFilePaste.Enabled = true;
                    else cmiFilePaste.Enabled = false;
                    ctxtBackRightClick.Show(Cursor.Position);
                }
            }
        }

        private void StartRenameEntry(object sender, LabelEditEventArgs e) {
            _labelOld = ((ListView)sender).Items[e.Item].Text;
        }

        private void EndRenameEntry(object sender, LabelEditEventArgs e) {
            if (e.Label != null) {
                if (!File.Exists(CurrentDirectory + e.Label)) {
                    File.Move(CurrentDirectory + _labelOld, CurrentDirectory + e.Label);
                } else {
                    e.CancelEdit = true;
                    MessageBox.Show("A file with that name already exists!", "File Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Context Menu Events

        //TODO: These should be added from plugins (these specific ones from the default plugin)
        private void cmiFileOpen_Click(object sender, EventArgs e) {
            ListViewItem selectedItem = lstFiles.SelectedItems[0];
            IOFunctions.OpenEntry(this, selectedItem);
        }

        private void cmiFileRename_Click(object sender, EventArgs e) {
            ListViewItem selectedItem = lstFiles.SelectedItems[0];
            selectedItem.BeginEdit();
        }

        private void cmiFileDelete_Click(object sender, EventArgs e) {
            IOFunctions.DeleteEntries(this, lstFiles.SelectedItems);
        }

        private void cmiFileCut_Click(object sender, EventArgs e) {
            IOFunctions.CutEntries(this, lstFiles.SelectedItems);
        }

        private void cmiFileCopy_Click(object sender, EventArgs e) {
            IOFunctions.CopyEntries(this, lstFiles.SelectedItems);
        }

        private void cmiFilePaste_Click(object sender, EventArgs e) {
            IOFunctions.PasteFromClipboard(this);
        }


        #endregion
    }

    // this is used to see if the update thread is running
    public static class GlobalVars
    {
        public static bool isUpdating = false;
    }
}

