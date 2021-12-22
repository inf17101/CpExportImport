using System;
using System.IO;
using System.Collections.Generic;
using CpImportExportLibrary.src.ApiOperations;
using CpImportExportLibrary.src.Helpers;
using CpImportExportLibrary.src.Parser;
using CpImportExportLibrary.src.FileWriter;

namespace CpImportExportLibrary.src.Export
{
    public class ObjectExporter : IObjectExporter
    {
        public ISession _session { get; set; }
        public Dictionary<string, string> _predefinedObjects { get; set; }

        public Dictionary<string, string> _alreadyAddedItems { get; set; }

        public List<string> _ignoredBuiltInTypes { get; set; }

        public List<string> _itemsToRemove { get; set; }

        public string _rootDir { get; set; }

        public ObjectExporter(ISession session,
                                Dictionary<string, string> predefinedObjects,
                                    Dictionary<string, string> alreadyAddedItems,
                                        List<string> ignoredBuiltInTypes, List<string> itemsToRemove, string rootDir)
        {
            _session = session;
            _predefinedObjects = predefinedObjects;
            _alreadyAddedItems = alreadyAddedItems;
            _ignoredBuiltInTypes = ignoredBuiltInTypes;
            _itemsToRemove = itemsToRemove;
            _rootDir = rootDir;
        }

        private bool MustBeExported(dynamic item)
        {
            if (item.ContainsKey("name") && item.ContainsKey("type"))
            {
                string itemName = Convert.ToString(item["name"]);
                string itemType = Convert.ToString(item["type"]);
                return !(_predefinedObjects.ContainsKey(itemName) && Convert.ToString(_predefinedObjects[itemName]) == itemType) &&
                !_alreadyAddedItems.ContainsKey(itemName) && !_ignoredBuiltInTypes.Contains(itemType);
            }
            return false;
        }

        public void ExportObject(dynamic item)
        {
            if (MustBeExported(item))
            {
                SearchReplace sr = new SearchReplace();
                sr.RemoveProperties(_itemsToRemove, item);
                string apiType = Convert.ToString(item["type"]);
                string itemName = Convert.ToString(item["name"]);

                IParser parser = ParserFactory.CreateParser(apiType);
                parser.Parse(item, this);

                item.Remove("uid");
                /* export object to File */
                string objectDir = _rootDir + "Objects/" + apiType;
                Directory.CreateDirectory(objectDir);
                FileExporter.ExportToFile(objectDir + $"/{apiType}.txt", item.ToString(), delemiter: "\n---\n");

                _alreadyAddedItems.Add(itemName, apiType);
                item.Add("ignore-warnings", true);
                item.Remove("type");
            }
        }
    }
}