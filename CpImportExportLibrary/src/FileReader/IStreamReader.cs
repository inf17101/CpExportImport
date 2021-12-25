using System.IO;

namespace CpImportExportLibrary.src.FileReader
{
    public interface IStreamReader
    {
        StreamReader GetReader(string path);
    }
}
