using System.IO;

namespace ExplodePluginBase
{
    public interface IPluginBase
    {
        string FriendlyName { get; }
        string ColumnData(FileStream stream);
    }

    public interface IFileTypeBase
    {
        string CheckFileType(FileStream stream);
    }
}
