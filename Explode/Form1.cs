using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ExplodePluginBase;

namespace Explode
{
    public partial class Form1 : Form
    {
        // Creates a new plugin manager system and loads plugins
        public PluginManager manager;
        private string directory;
        private List<string> pathHistory = new List<string>();

        public Form1()
        {
            InitializeComponent();
            manager = new PluginManager(Directory.GetCurrentDirectory(), listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentDirectory = "C:/Users";
        }

        // this property controls directory changes and input validation
        public string CurrentDirectory
        {
            get { return directory; }
            set
            {
                // makes sure the new folder actually exists
                if (Directory.Exists(value))
                {
                    //verify the user has sufficient access to the directory
                    //this will trigger on certain special pointer directories (ie. legacy symlinks like Application Data)
                    try {
                        Directory.GetFileSystemEntries(value);
                    } catch (UnauthorizedAccessException) {
                        MessageBox.Show("You don't have permission to access this directory.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    pathHistory.Add(directory);
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        FileUpdateHandler.UpdateUI(this);
                    }).Start();

                }
            }
        }

        
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                CurrentDirectory = textBox1.Text;
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = listView1.SelectedItems[0];
            if (Directory.Exists(CurrentDirectory + selectedItem.Text))
            {
                CurrentDirectory += selectedItem.Text;
            }
            else
            {
                // execute the file using plugins, if defined
                FileStream file = File.OpenRead(CurrentDirectory + selectedItem.Text);
                foreach (IFileTypeBase type in manager.FileTypes)
                {                    
                    if (type.CheckFileType(file) != null)
                    {
                        if (type.ExecuteFile(file) == 0)
                        {
                            System.Diagnostics.Process.Start(file.Name);
                        }
                        file.Close();
                        break;
                    }
                }

                if (file.CanRead)
                {
                    // this happens if a file type has no plugin to control it
                    System.Diagnostics.Process.Start(file.Name);
                }
            }

        }


        // called when back button pressed
        private void GoBack(object sender, EventArgs e)
        {
            // makes sure there is a path to go back to
            if (pathHistory.Count > 1)
            {
                CurrentDirectory = pathHistory[pathHistory.Count - 2];
                // since setting CurrentDirectory adds to the undo history, we need to remove the entry
                pathHistory.RemoveRange(pathHistory.Count - 2, 2);
            }
            else
            {
                // this means that we've reached the root
                MessageBox.Show("You've reached the end of your back history.", "No more to undo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}