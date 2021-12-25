using System;
using System.Collections.Generic;

namespace CpImportExportLibrary.src.FileReader
{
    public class PredefinedObjectsReader
    {
        private IStreamReader _streamReader;
        private string _filename;
        private char _delimiter;
        public PredefinedObjectsReader(IStreamReader streamReader, string filename, char delimiter)
        {
            _streamReader = streamReader;
            _filename = filename;
            _delimiter = delimiter;
        }
        private bool HasLengthOf(string[] array, int len)
        {
            return array.Length == len;
        }

        private void AddToDict(ref Dictionary<string, string> dict, string key, string value)
        {   
            if(!dict.ContainsKey(key))
                dict.Add(key, value);
        }
        public Dictionary<string,string> ReadPredefinedObjects()
        {
            var predefinedObjects = new Dictionary<string, string>();
            try
            {
                using var reader = _streamReader.GetReader(_filename);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] substrings = line.Split(_delimiter);
                    if (!(HasLengthOf(substrings, 2)))
                        throw new FormatException("Predefined Objects File seems to be corrupted.");

                    AddToDict(ref predefinedObjects, substrings[0], substrings[1]);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR: Could not read file.\nCause:{e.Message}");
                return new Dictionary<string, string>();
            }
            return predefinedObjects;
        }
    }
}