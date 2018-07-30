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
using System.Threading;
using ExplodePluginBase;

namespace Explode {
    public partial class FormMain : Form {
        

        // Creates a new plugin manager system and loads plugins
        public PluginManager manager;
        private string directory;

        //history stuff
        public List<string> pathHistory = new List<string>();
        int historyPointer = 0;
        bool updateHistory = true;

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
                        if (pathHistory.Count() - 1 > historyPointer + 1) pathHistory.RemoveRange(historyPointer + 1, pathHistory.Count() - 1 - historyPointer);
                        pathHistory.Add(directory);
                        historyPointer = pathHistory.Count() - 1;
                        
                    }

                    if (historyPointer == 0) btnBack.Enabled = false;
                    else btnBack.Enabled = true;

                    if (historyPointer == pathHistory.Count() - 1) btnForward.Enabled = false;
                    else btnForward.Enabled = true;
                }
            }
        }

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
            if (Directory.Exists(CurrentDirectory + selectedItem.Text)) {
                CurrentDirectory += selectedItem.Text;
            } else {
                // execute the file using plugins, if defined
                FileStream file = File.OpenRead(CurrentDirectory + selectedItem.Text);
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

        private void btnGoUp_Click(object sender, EventArgs e) {
            CurrentDirectory = new DirectoryInfo(CurrentDirectory).Parent.FullName;
        }
    }
}

