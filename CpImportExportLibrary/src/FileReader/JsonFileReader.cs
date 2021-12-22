using System;
using System.IO;
namespace CpImportExportLibrary.src.FileReader
{
    public class JsonFileReader
    {
        public string ReadContent(string path)
        {
            string fileContent = "";
            try
            {
                using var reader = new StreamReader(path);
                fileContent = reader.ReadToEnd();
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Could not read file.\nCause:{e.Message}");
                return fileContent;
            }

            return fileContent;
        }
    }
}