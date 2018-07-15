using System;
using System.IO;
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
}

