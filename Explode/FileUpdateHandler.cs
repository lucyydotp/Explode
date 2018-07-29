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

                    foreach (ExplodeColumn column in target.listView1.Columns)
                    {
                        if (column.Text != "Name")
                        {
                            target.Invoke(new Action(() =>
                                target.listView1.Items[index].SubItems.Add(column.GetInfo(handle))));
                        }
                    }

                    handle.Close();
                }
                // this happens if it's a directory or can't be accessed
                catch (UnauthorizedAccessException e)
                {
                    int length = 0;
                    target.Invoke(new Action(() => length = target.listView1.Items[index].SubItems.Count));
                    for (int x = 1; x == length; x++)
                    {
                        target.Invoke(new Action(() => target.listView1.Items[index].SubItems.Add("")));
                    }
                }
                // this means that it's trying to process the name column, which we can ignore
                catch (InvalidCastException e)
                {
                    ;
                }
                // I know that this is bad practice but let's do it anyway
                catch (Exception e)
                {
                    ;
                }

                index++;
            }

            target.Invoke(new Action(() => target.listView1.EndUpdate()));
        }
    }
}
