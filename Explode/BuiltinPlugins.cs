using System;
using System.IO;
using System.Reflection;
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
        public string FriendlyName
        {
            get { return "Extension"; }
        }

        public string ColumnData(FileStream s)
        {
            return Path.GetExtension(s.Name);
        }
    }
}

