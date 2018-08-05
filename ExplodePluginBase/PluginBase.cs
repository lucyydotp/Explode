using System.IO;
using System.Windows.Forms;

namespace ExplodePluginBase
{
    public interface IPluginMeta
    {
        string FriendlyName { get; }
        double Version { get; }
        string Author { get; }
        string Website { get; }
    }

    // column plugins
    public interface IPluginBase : IPluginMeta
    {
        string ColumnData(FileStream stream);
        ColumnHeaderAutoResizeStyle ColumnWidth { get; }
    }

    // file type plugins
    public interface IFileTypeBase : IPluginMeta
    {
        string CheckFileType(FileStream stream);
        int ExecuteFile(FileStream stream);
    }

    // right click items
    public interface IMenuItemBase : IPluginMeta
    {
        string ActionName { get; }
        void ProcessFile(FileStream stream);

    }
}
