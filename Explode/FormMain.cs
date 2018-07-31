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

namespace Explode {
    public partial class FormMain : Form {
        

        // Creates a new plugin manager system and loads plugins
        public PluginManager manager;
        private string directory;

        //history stuff
        public List<string> pathHistory = new List<string>();
        int historyPointer = -1;
        bool updateHistory = true;

        private string _labelOld;

        public FormMain() {
            InitializeComponent();
            manager = new PluginManager(Directory.GetCurrentDirectory(), lstFiles);
            
        }        

        // this property controls directory changes and input validation
        public string CurrentDirectory {
            get { return directory; }
            set {
                // makes sure the new folder actually exists
                if (Directory.Exists(value)) {
                    //verify the user has sufficient access to the directory
                    //this will trigger on certain special pointer directories (ie. legacy symlinks like Application Data)
                    try {
                        Directory.GetFileSystemEntries(value);
                    } catch (UnauthorizedAccessException) {
                        MessageBox.Show("You don't have permission to access this directory.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // if it doesn't end with a /, add one
                    if (value.Replace("\\", "/").EndsWith("/") == false) {
                        directory = value.Replace("\\", "/") + "/";
                    } else {
                        directory = value.Replace("\\", "/");
                    }

                    new Thread(() => {
                        Thread.CurrentThread.IsBackground = true;
                        FileUpdateHandler.UpdateUI(this);
                    }).Start();

                    if (updateHistory) {
                        historyPointer++;
                        if (historyPointer <= pathHistory.Count() - 1) pathHistory.RemoveRange(historyPointer, pathHistory.Count() - historyPointer);
                        pathHistory.Add(directory);
                        
                    }

                    if (historyPointer == 0) btnBack.Enabled = false;
                    else btnBack.Enabled = true;

                    if (historyPointer == pathHistory.Count() - 1) btnForward.Enabled = false;
                    else btnForward.Enabled = true;
                }
            }
        }

        #region File Operations

        private void OpenEntry(ListViewItem item) {
            if (Directory.Exists(CurrentDirectory + item.Text)) {
                CurrentDirectory += item.Text;
            } else {
                // execute the file using plugins, if defined
                FileStream file = File.OpenRead(CurrentDirectory + item.Text);
                foreach (IFileTypeBase type in manager.FileTypes) {
                    if (type.CheckFileType(file) != null) {
                        if (type.ExecuteFile(file) == 0) {
                            System.Diagnostics.Process.Start(file.Name);
                        }
                        file.Close();
                        break;
                    }
                }

                if (file.CanRead) {
                    // this happens if a file type has no plugin to control it
                    System.Diagnostics.Process.Start(file.Name);
                    file.Close();
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

        private void CutEntries(ListView.SelectedListViewItemCollection items) {
            System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
            foreach (ListViewItem k in items) paths.Add(CurrentDirectory + k.Text);

            byte[] moveEffect = { 2, 0, 0, 0 };
            MemoryStream dropEffect = new MemoryStream();
            dropEffect.Write(moveEffect, 0, moveEffect.Length);

            DataObject dataObj = new DataObject("Preferred DropEffect", dropEffect);
            dataObj.SetFileDropList(paths);

            Clipboard.Clear();
            Clipboard.SetDataObject(dataObj, true);
        }

        private void CopyEntries(ListView.SelectedListViewItemCollection items) {
            System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
            foreach (ListViewItem k in items) paths.Add(CurrentDirectory + k.Text);
            Clipboard.SetFileDropList(paths);
        }

        private void DeleteEntries(ListView.SelectedListViewItemCollection items) {
            foreach(ListViewItem k in items) {
                if (File.Exists(CurrentDirectory + k.Text)) File.Delete(CurrentDirectory + k.Text);
                else Directory.Delete(CurrentDirectory + k.Text);
                lstFiles.Items.Remove(k);
            }
        }

        #endregion

        #region Form Events

        private void FormMain_Load(object sender, EventArgs e) {
            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                FileUpdateHandler.UpdateQuickAccess(this);
            }).Start();
            CurrentDirectory = "C:/Users";
        }

        private void txtCurrentDirectory_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                CurrentDirectory = txtCurrentDirectory.Text;
            }
        }

        private void lstFiles_ItemActivate(object sender, EventArgs e) {
            ListViewItem selectedItem = lstFiles.SelectedItems[0];
            OpenEntry(selectedItem);
        }

        // called when back button pressed
        private void GoBack(object sender, EventArgs e) {
            updateHistory = false;
            historyPointer--;
            CurrentDirectory = pathHistory[historyPointer];
            updateHistory = true;
        }

        private void GoForward(object sender, EventArgs e) {
            updateHistory = false;
            historyPointer++;
            CurrentDirectory = pathHistory[historyPointer];
            updateHistory = true;
        }

        private void Refresh(object sender, EventArgs e) {
            new Thread(() => {
                Thread.CurrentThread.IsBackground = true;
                FileUpdateHandler.UpdateUI(this);
            }).Start();
        }

        private void btnGoUp_Click(object sender, EventArgs e)
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

        #endregion

        #region Context Menu Events

        //TODO: These should be added from plugins (these specific ones from the default plugin)
        private void cmiFileOpen_Click(object sender, EventArgs e) {
            ListViewItem selectedItem = lstFiles.SelectedItems[0];
            OpenEntry(selectedItem);
        }

        private void cmiFileRename_Click(object sender, EventArgs e) {
            ListViewItem selectedItem = lstFiles.SelectedItems[0];
            selectedItem.BeginEdit();
        }

        private void cmiFileDelete_Click(object sender, EventArgs e) {
            DeleteEntries(lstFiles.SelectedItems);
        }

        private void cmiFileCut_Click(object sender, EventArgs e) {
            CutEntries(lstFiles.SelectedItems);
        }

        private void cmiFileCopy_Click(object sender, EventArgs e) {
            CopyEntries(lstFiles.SelectedItems);
        }

        private void cmiFilePaste_Click(object sender, EventArgs e) {
            byte[] moveType = new byte[4];
            bool cut = false;

            //check if the dropeffect specifies to cut the files or not
            if ((Clipboard.GetData("Preferred DropEffect") as MemoryStream) != null) {
                if ((Clipboard.GetData("Preferred DropEffect") as MemoryStream).Read(moveType, 0, 4) == 4 && moveType.SequenceEqual(new byte[] { 2, 0, 0, 0 })) {
                    cut = true;
                }
            }

            //TODO: actually do stuff with the files
            //need a some way to display progress
            //this should also be asynchronus
        }
        #endregion
    }
}

