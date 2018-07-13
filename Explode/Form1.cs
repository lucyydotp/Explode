using System;
using System.IO;
using System.Windows.Forms;
using ExplodePluginBase;

namespace Explode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CurrentDirectory = "C:/";
        }

        // Creates a new plugin manager system and loads plugin
        PluginManager manager = new PluginManager(Directory.GetCurrentDirectory());
        private string directory;

        // this property controls directory changes and input validation
        private string CurrentDirectory
        {
            get { return directory; }
            set
            {
                // makes sure the new folder actually exists
                if (Directory.Exists(value))
                {
                    // if it doesn't end with a /, add one
                    if (value.Replace("\\", "/").EndsWith("/") == false)
                    {
                        directory = value.Replace("\\", "/") + "/";
                    }
                    else
                    {
                        directory = value.Replace("\\", "/");
                    }
                    // update the UI with the new directory
                    listView1.Clear();
                    textBox1.Text = directory;
                    int index = 0;
                    foreach (string item in Directory.GetFileSystemEntries(directory))
                    {
                        listView1.Items.Add(item);
                        try
                        {
                            listView1.Items[index].SubItems.Add(getType(File.OpenRead(item)));
                        } catch (Exception e)
                        {
                            listView1.Items[index].SubItems.Add("Error");
                        }

                        index++;
                    }

                }
            }
        }

        private string getType(FileStream file)
        {
            string data = "Unknown type";
            foreach (IFileTypeBase plugin in manager.FileTypes)
            { 
                if (plugin.CheckFileType(file) != null)
                {
                    data = plugin.CheckFileType(file);
                }
            }

            return data;
        }
    }
}
