using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Explode;
using ExplodePluginBase;

/* BUILTIN PLUGIN LIST:
 Text File
 Shortcut
 *
 */

namespace Explode
{
    public class BuiltinTxt : IFileTypeBase
    {
        public string FriendlyName { get { return "Text File"; }}
        public string Author { get { return "Explode"; } }
        public double Version { get { return 1.00; } }
        public string Website { get { return "https://github.com/SamPoulton/Explode"; } }

        public string CheckFileType(FileStream s)
        {
            string data = null;
            if (Path.GetExtension(s.Name) == ".txt")
            {
                data = "Text File";
            }
            return data;
        }
        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinLnk : IFileTypeBase
    {
        public string FriendlyName { get { return "Shortcut"; } }
        public string Author { get { return "Explode"; } }
        public double Version { get { return 1.00; } }
        public string Website { get { return "https://github.com/SamPoulton/Explode"; } }
        public string CheckFileType(FileStream s)
        {
            string data = null;
            if (Path.GetExtension(s.Name) == ".lnk")
            {
                data = "Shortcut";
            }

            return data;
        }
        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinExe : IFileTypeBase
    {
        public string FriendlyName { get { return "Windows PE Files"; } }
        public string Author { get { return "Explode"; } }
        public double Version { get { return 1.00; } }
        public string Website { get { return "https://github.com/SamPoulton/Explode"; } }
        public string CheckFileType(FileStream s)
        {
            string data = null;
            char[] buffer = new char[4];
            string magicNumbers = "MZ";
            StreamReader reader = new StreamReader(s);
            reader.Read(buffer, 0, 2);
            string bufferString = new string(buffer);
            if (bufferString == magicNumbers)
            {
                if (Path.GetExtension(s.Name) == ".exe")
                {
                    data = "Program";
                } else if (Path.GetExtension(s.Name) == ".dll")
                {
                    data = "Dynamic Link Library";
                }
            }

            return data;
        }
        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    // Builtin columns

    public class BuiltinName : IPluginBase
    {
        public string Author
        {
            get { return "Explode"; }
        }
        public double Version
        {
            get { return 1.00; }
        }
        public string Website
        {
            get { return "https://github.com/SamPoulton/Explode"; }
        }
        public ColumnHeaderAutoResizeStyle ColumnWidth
        {
            get
            {
                return ColumnHeaderAutoResizeStyle.ColumnContent;
            }
        }
        public string FriendlyName
        {
            get { return "Name"; }
        }

        public string ColumnData(FileStream s)
        {
            return Path.GetFileName(s.Name);
        }
    }

    public class BuiltinSize : IPluginBase
    {
        public string Author
        {
            get { return "Explode"; }
        }
        public double Version
        {
            get { return 1.00; }
        }
        public string Website
        {
            get { return "https://github.com/SamPoulton/Explode"; }
        }
        public ColumnHeaderAutoResizeStyle ColumnWidth
        {
            get
            {
                return ColumnHeaderAutoResizeStyle.ColumnContent;
            }
        }
        public string FriendlyName
        {
            get { return "Size"; }
        }

        public string ColumnData(FileStream s)
        {
            long byteCount = s.Length;
            string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1000)));
            double num = Math.Round(bytes / Math.Pow(1000, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }

    public class BuiltinFormat : IPluginBase
    {
        public string Author
        {
            get { return "Explode"; }
        }
        public double Version
        {
            get { return 1.00; }
        }
        public string Website
        {
            get { return "https://github.com/SamPoulton/Explode"; }
        }
        public ColumnHeaderAutoResizeStyle ColumnWidth
        {
            get
            {
                return ColumnHeaderAutoResizeStyle.ColumnContent;
            }
        }
        public string FriendlyName
        {
            get { return "Format"; }
        }

        public string ColumnData(FileStream s)
        {
            string data = "Unknown type";
            foreach (IFileTypeBase plugin in Program.app.manager.FileTypes)
            {
                s.Position = 0;
                string checkFileType = plugin.CheckFileType(s);
                if (checkFileType != null)
                {
                    data = checkFileType;
                    break;
                }
            }

            return data;
        }
    }

    public class BuiltinExtension : IPluginBase
    {
        public string Author
        {
            get { return "Explode"; }
        }
        public double Version
        {
            get { return 1.00; }
        }
        public string Website
        {
            get { return "https://github.com/SamPoulton/Explode"; }
        }
        public ColumnHeaderAutoResizeStyle ColumnWidth
        {
            get
            {
                return ColumnHeaderAutoResizeStyle.HeaderSize;
            }
        }
        public string FriendlyName
        {
            get { return "Extension"; }
        }

        public string ColumnData(FileStream s)
        {
            return Path.GetExtension(s.Name);
        }
    }

    // Test right-click menu items

    public class BuiltinTestItem : IMenuItemBase
    {
        public string FriendlyName { get { return "Test Plugin"; } }
        public string ActionName { get { return "Test"; } }
        public string Author { get { return "Explode"; } }
        public double Version { get { return 1.00; } }
        public string Website { get { return "https://github.com/SamPoulton/Explode"; } }

        public void ProcessFile(FileStream stream)
        {
            Debug.WriteLine("Item '" + stream.Name + "' tested");
        }
    }
}

