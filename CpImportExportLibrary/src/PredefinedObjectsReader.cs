using System;
using System.IO;
using System.Collections.Generic;

namespace CpExportImport
{
    static class PredefinedObjectsReader
    {
        private static bool HasLengthOf(string[] array, int len)
        {
            return array.Length == len;
        }

        private static void AddToDict(ref Dictionary<string, string> dict, string key, string value)
        {   
            if(!dict.ContainsKey(key))
                dict.Add(key, value);
        }
        public static Dictionary<string,string> ReadPredefinedObjects(string filename, string delemiter)
        {
            var predefinedObjects = new Dictionary<string, string>();
            try
            {
                using(var reader = new StreamReader(filename))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        string[] substrings = line.Split(delemiter);
                        if(!(HasLengthOf(substrings, 2)))
                            throw new FormatException("The File seems to be corrupted.");
                            
                        AddToDict(ref predefinedObjects, substrings[0], substrings[1]);
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine($"ERROR: Could not read file.\nCause:{e.Message}");
                return predefinedObjects;
            }
            return predefinedObjects;
        }
    }
}