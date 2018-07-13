using System.IO;
using ExplodePluginBase;

namespace FirstPlugin
{
    public class FirstPlugin : IPluginBase
    {
        public string CheckFileType(FileStream file)
        {
            return file.Name;
        }

        public string FriendlyName
        {
            get { return "Example Plugin"; }
        }
    }
}