using System;
using System.IO;

namespace CpImportExportLibrary.src.FileWriter
{
    class JsonFileWriter
    {
        public void Json2File(string path, string jsonString)
        {
            try
            {
                using var writer = new StreamWriter(path);
                writer.Write(jsonString);
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Could not write file.\nCause:{e.Message}");
            }
        }
    }
}
