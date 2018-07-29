using System.IO;
using ExplodePluginBase;

namespace ExamplePlugin
{
    public class ExamplePlugin : IPluginBase
    {
        public string ColumnData(FileStream file)
        {
            char[] buffer = new char[4];
            new StreamReader(file).Read(buffer,0,4);
            return new string(buffer);
        }

        public string FriendlyName
        {
            get { return "First 4"; }
        }
    }
}