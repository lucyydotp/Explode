using System;
using System.IO;
using ExplodePluginBase;

/* PLUGIN LIST:
 Yaz0 Archive
 SARC Archive
 BYML File
 BFRES Archive
 BARS Archive
 BFSTM Sound
 */

namespace ExplodeNintendoPlugins
{
    public class BuiltinYaz0 : IFileTypeBase
    {
        public string CheckFileType(FileStream file)
        {
            char[] buffer = new char[4];
            string magicNumbers = "Yaz0";
            StreamReader reader = new StreamReader(file);
            reader.Read(buffer, 0, 4);
            string bufferString = new string(buffer);
            if (bufferString == magicNumbers)
            {
                return "Nintendo Yaz0 Archive";

            }

            return null;
        }

        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinSarc : IFileTypeBase
    {
        public string CheckFileType(FileStream file)
        {
            char[] buffer = new char[4];
            string magicNumbers = "SARC";
            StreamReader reader = new StreamReader(file);
            reader.Read(buffer, 0, 4);
            string bufferString = new string(buffer);
            if (bufferString == magicNumbers)
            {
                return "Nintendo SARC Archive";

            }

            return null;
        }

        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinByml : IFileTypeBase
    {
        public string CheckFileType(FileStream file)
        {
            char[] buffer = new char[2];
            StreamReader reader = new StreamReader(file);
            reader.Read(buffer, 0, 2);
            string bufferString = new string(buffer);
            if (bufferString == "BY")
            {
                return "Nintendo BYML File";

            }

            if (bufferString == "YB")
            {
                return "Little Endian Nintendo BYML File";
            }

            return null;
        }

        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinBfres : IFileTypeBase
    {
        public string CheckFileType(FileStream file)
        {
            char[] buffer = new char[4];
            string magicNumbers = "FRES";
            StreamReader reader = new StreamReader(file);
            reader.Read(buffer, 0, 4);
            string bufferString = new string(buffer);
            if (bufferString == magicNumbers)
            {
                return "Nintendo BFRES Archive";

            }

            return null;
        }

        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinBars : IFileTypeBase
    {
        public string CheckFileType(FileStream file)
        {
            char[] buffer = new char[4];
            string magicNumbers = "BARS";
            StreamReader reader = new StreamReader(file);
            reader.Read(buffer, 0, 4);
            string bufferString = new string(buffer);
            if (bufferString == magicNumbers)
            {
                return "Nintendo BARS Archive";

            }

            return null;
        }

        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }

    public class BuiltinBfstm : IFileTypeBase
    {
        public string CheckFileType(FileStream file)
        {
            char[] buffer = new char[4];
            string magicNumbers = "FSTM";
            StreamReader reader = new StreamReader(file);
            reader.Read(buffer, 0, 4);
            string bufferString = new string(buffer);
            if (bufferString == magicNumbers)
            {
                return "Nintendo BFSTM Sound";

            }

            return null;
        }

        public int ExecuteFile(FileStream f)
        {
            return 0;
        }
    }
}
