using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExplodePluginBase;
using System.Diagnostics;

namespace Explode {
    public static class IOFunctions {

        public static void OpenEntry(FormMain form, string path) {
            if (Directory.Exists(form.CurrentDirectory + path)) {
                form.CurrentDirectory += path;
            } else {
                // execute the file using plugins, if defined
                FileStream file = File.OpenRead(form.CurrentDirectory + path);
                foreach (IFileTypeBase type in form.manager.FileTypes) {
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

        public static void OpenEntry(FormMain form, ListViewItem item) {
            OpenEntry(form, item.Text);
        }

        public static void CutEntries(FormMain form, ListView.SelectedListViewItemCollection items) {
            System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
            foreach (ListViewItem k in items) paths.Add(form.CurrentDirectory + k.Text);

            byte[] moveEffect = { 2, 0, 0, 0 };
            MemoryStream dropEffect = new MemoryStream();
            dropEffect.Write(moveEffect, 0, moveEffect.Length);

            DataObject dataObj = new DataObject("Preferred DropEffect", dropEffect);
            dataObj.SetFileDropList(paths);

            Clipboard.Clear();
            Clipboard.SetDataObject(dataObj, true);
        }

        public static void CopyEntries(FormMain form, ListView.SelectedListViewItemCollection items) {
            System.Collections.Specialized.StringCollection paths = new System.Collections.Specialized.StringCollection();
            foreach (ListViewItem k in items) paths.Add(form.CurrentDirectory + k.Text);
            Clipboard.SetFileDropList(paths);
        }

        public static void DeleteEntries(FormMain form, ListView.SelectedListViewItemCollection items) {
            foreach (ListViewItem k in items) {
                if (File.Exists(form.CurrentDirectory + k.Text)) File.Delete(form.CurrentDirectory + k.Text);
                else Directory.Delete(form.CurrentDirectory + k.Text);
                form.lstFiles.Items.Remove(k);
            }
        }

        public static void PasteFromClipboard(FormMain form) {
            byte[] moveType = new byte[4];
            bool cut = false;

            //check if the dropeffect specifies to cut the files or not
            if ((Clipboard.GetData("Preferred DropEffect") as MemoryStream) != null) {
                if ((Clipboard.GetData("Preferred DropEffect") as MemoryStream).Read(moveType, 0, 4) == 4 && moveType.SequenceEqual(new byte[] { 2, 0, 0, 0 })) {
                    cut = true;
                }
            }

            Debug.WriteLine("Paste files cutted: " + cut);

            //TODO: actually do stuff with the files
            //need a some way to display progress
            //this should also be asynchronus
        }

    }
}
