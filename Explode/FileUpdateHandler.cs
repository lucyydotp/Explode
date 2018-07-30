using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Explode
{
    static class FileUpdateHandler
    {

        private static Dictionary<string, System.Drawing.Icon> iconCache = new Dictionary<string, System.Drawing.Icon>();

        public static void UpdateUI(FormMain target)
        {
            int index = 0;
            //clear directory listing, disable listview renders, update directory in path textbox, and reset the listview images
            target.Invoke(new Action(() => {
                target.lstFiles.Items.Clear();
                target.lstFiles.BeginUpdate();
                target.txtCurrentDirectory.Text = target.CurrentDirectory;
                target.lstFiles.SmallImageList = new ImageList();
                target.lstFiles.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            }));

            //this will ensure directories are put at the top of the list, and still have all special directories shown
            List<string> paths = new List<string>();
            paths.AddRange(Directory.GetDirectories(target.CurrentDirectory));
            paths.AddRange(Directory.GetFiles(target.CurrentDirectory));
            paths.AddRange(Directory.GetFileSystemEntries(target.CurrentDirectory).Except(paths));

            ListView _lv = new ListView();
            ImageList icons = new ImageList();
            icons.ColorDepth = ColorDepth.Depth32Bit;
            ListView.ListViewItemCollection items = new ListView.ListViewItemCollection(_lv);

            foreach (string item in paths)
            {
                //Grab the icon associated with the filetype/directory and add it to the directory image list. Then add the related item to the file listing

                try {

                    if (File.Exists(item)) icons.Images.Add(iconCache[new FileInfo(item).Extension]);
                    else icons.Images.Add(iconCache["|"]); // | cannot be put into filenames, therefore it is a safe unique char for directories

                } catch (KeyNotFoundException) {
                    if (File.Exists(item)) iconCache[new FileInfo(item).Extension] = Etier.IconHelper.IconReader.GetFileIcon(item, Etier.IconHelper.IconReader.IconSize.Small, false);
                    else iconCache["|"] = Etier.IconHelper.IconReader.GetFolderIcon(Etier.IconHelper.IconReader.IconSize.Small, Etier.IconHelper.IconReader.FolderType.Closed);

                    if (File.Exists(item)) icons.Images.Add(iconCache[new FileInfo(item).Extension]);
                    else icons.Images.Add(iconCache["|"]); // | cannot be put into filenames, therefore it is a safe unique char for directories
                }

                items.Add(item.Replace(target.CurrentDirectory, ""), icons.Images.Count - 1);

                try
                {
                    FileStream handle = File.OpenRead(item);

                    foreach (ExplodeColumn column in target.lstFiles.Columns)
                    {
                        if (column.Text != "Name")
                        {
                            items[index].SubItems.Add(column.GetInfo(handle));
                        }
                    }

                    handle.Close();
                }
                // this happens if it's a directory or can't be accessed
                catch (UnauthorizedAccessException e)
                {
                    int length = items[index].SubItems.Count;

                    for (int x = 1; x == length; x++)
                    {
                        items[index].SubItems.Add("");
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

            target.Invoke(new Action(() => {
                //TODO: These should be decided by the plugin adding the column.
                while (items.Count > 0) {
                    ListViewItem k = items[0];
                    items.RemoveAt(0);
                    target.lstFiles.Items.Add(k);
                }
                target.lstFiles.Items.AddRange(items);
                target.lstFiles.SmallImageList = icons;

                target.lstFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                target.lstFiles.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.HeaderSize);
                target.lstFiles.EndUpdate();

                }));
        }

        public static void UpdateQuickAccess(FormMain target) {
            target.Invoke(new Action(() => {
                target.lstQuickAccess.Groups.Clear();
                target.lstQuickAccess.Items.Clear();
                target.lstQuickAccess.BeginUpdate();
            }));


            ListViewGroup drives = new ListViewGroup("Drives", HorizontalAlignment.Left);
            target.Invoke(new Action(() => target.lstQuickAccess.Groups.Add(drives)));
            foreach (DriveInfo k in DriveInfo.GetDrives()) {
                ListViewItem item;
                try {
                    item = new ListViewItem(new string[] { "", k.VolumeLabel + " (" + k.RootDirectory + ")" }, target.lstQuickAccess.Groups[0]);
                } catch (IOException) {
                    item = new ListViewItem(new string[] { "", "Unknown (" + k.RootDirectory + ")" }, target.lstQuickAccess.Groups[0]);
                }
                target.Invoke(new Action(() => {
                    target.lstQuickAccess.Items.Add(item);
                    target.lstQuickAccess.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    target.lstQuickAccess.EndUpdate();
                }));
            }
        }
    }
}
