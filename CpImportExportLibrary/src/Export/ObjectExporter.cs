using System;
using System.IO;
using System.Collections.Generic;
using CpImportExportLibrary.src.ApiOperations;
using CpImportExportLibrary.src.Helpers;
using CpImportExportLibrary.src.Parser;
using CpImportExportLibrary.src.FileWriter;

namespace CpImportExportLibrary.src.Export
{
    public class ObjectExporter
    {
        public Session _session { get; set; }
        public Dictionary<string, string> _predefinedObjects { get; set; }

        public Dictionary<string, string> _alreadyAddedItems { get; set; }

        public List<string> _ignoredBuiltInTypes { get; set; }

        public List<string> _itemsToRemove { get; set; }

        public string _rootDir { get; set; }

        public ObjectExporter(Session session, 
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

        public bool MustBeExported(dynamic item)
        {
            if(item.ContainsKey("name") && item.ContainsKey("type"))
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
            if(MustBeExported(item))
            {
                SearchReplace sr = new SearchReplace();
                sr.RemoveProperties(_itemsToRemove, item);

                if(item["type"] == "group-with-exclusion")
                {
                    IParser parser = new ParserGroupWithExclusion();
                    parser.parse(item, this);
                }else if(item["type"] == "host")
                {
                    IParser parser = new ParserHostObject();
                    parser.parse(item, this);
                }else if(item["type"] == "network")
                {
                    IParser parser = new ParserNetwork();
                    parser.parse(item, this);
                }else if(item["type"] == "group")
                {
                    ParserGroup parser = new ParserGroup();
                    parser.parse(item, this);
                }else if(item["type"] == "application-site")
                {
                    IParser parser = new ParserApplicationSite();
                    parser.parse(item, this);
                }
                /*else if(isDataCenterObject(item))
                {
                    Parser parser = new ParserDataCenterObject();
                    parser.parse(item);
                }*/
                //else if(item["type"] == "updatable-object")
                //{
                    /*
                        steps:
                            - check if updatable object is present in the predefined objects
                            - if it is not present try to add it, otherwise remove the object out of the rulebase
                            - if the object is present or could be added, use this object in the rulebase
                            - if object was removed out of the rulebase print a userfriendly error message
                    */
                //    Parser parser = new ParserUpdatableObjects();
                //   parser.parse(item, this);
                //}

                item.Remove("uid");
                /* export object to File */
                string objectDir = _rootDir + "Objects/" + Convert.ToString(item["type"]);
                Directory.CreateDirectory(objectDir);
                FileExporter.ExportToFile(objectDir + $"/{item["type"]}.txt", item.ToString(), delemiter:"\n---\n");

                //Console.WriteLine(item["type"]);
                _alreadyAddedItems.Add(Convert.ToString(item["name"]), Convert.ToString(item["type"]));
                item.Add("ignore-warnings", true);
                item.Remove("type");
            }

        }
    }
}