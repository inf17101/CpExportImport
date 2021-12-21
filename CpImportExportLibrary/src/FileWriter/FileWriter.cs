using System.IO;

namespace CpExportImport
{
    static class FileWriter
    {
        /* 
            export object to file separated by delemiter in the file
            if file exists already the object is appended to the objects inside the file,
            otherwise the file is being created first.        
        */
        public static void ExportToFile(string path, string data, string delemiter)
        {
            if(!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(data + delemiter);
                }
                return;
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(data + delemiter);
            }	
        }
    }
}