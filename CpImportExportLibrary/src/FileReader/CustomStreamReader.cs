using System.IO;

namespace CpImportExportLibrary.src.FileReader
{
    public class CustomStreamReader : IStreamReader
    {
        public StreamReader GetReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
