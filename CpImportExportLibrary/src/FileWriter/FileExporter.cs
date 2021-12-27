using System.IO;

namespace CpImportExportLibrary.src.FileWriter
{
    public class FileExporter
    {
        /// <summary>
        /// export object to file separated by delemiter in the file
        /// if file exists already the object is appended to the objects inside the file,
        /// otherwise the file is being created first.    
        /// </summary>

        public static void ExportToFile(string path, string data, string delimiter)
        {
            if(!File.Exists(path))
            {
                using StreamWriter sw = File.CreateText(path);
                sw.Write(data + delimiter);
                return;
            }

            using StreamWriter swAppender = File.AppendText(path);
            swAppender.Write(data + delimiter);
        }
    }
}