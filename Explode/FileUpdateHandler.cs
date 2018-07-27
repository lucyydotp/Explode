using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Explode
{
    static class FileUpdateHandler
    {
        public static void UpdateUI(Form1 target)
        {
            target.Invoke(new Action(() => target.listView1.Items.Clear()));
            target.Invoke(new Action(() => target.textBox1.Text = target.CurrentDirectory));
            int index = 0;
            target.Invoke(new Action(() => target.listView1.BeginUpdate()));
            foreach (string item in Directory.GetFileSystemEntries(target.CurrentDirectory))
            {
                target.Invoke(new Action(() => target.listView1.Items.Add(item.Replace(target.CurrentDirectory, ""))));
                try
                {
                    FileStream handle = File.OpenRead(item);
                    target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add(target.getSize(handle.Length))));
                    target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add(target.getType(handle))));
                    target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add(Path.GetExtension(handle.Name))));
                    handle.Close();
                }
                // this happens if it's a directory or can't be accessed
                catch (UnauthorizedAccessException e)
                {
                    if (Directory.Exists(item))
                    {
                        target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add("")));
                        target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add("Folder")));
                        target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add("")));
                    }
                }
                catch (Exception e)
                {
                    continue;
                }

                index++;
            }

            target.Invoke(new Action(() => target.listView1.EndUpdate()));
        }
    }
}
