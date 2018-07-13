using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplodePluginBase
{
    public interface IPluginBase
    {
        string FriendlyName { get; }
        string CheckFileType(FileStream stream);
    }
}
