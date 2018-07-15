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
}
