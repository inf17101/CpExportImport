using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CpExportImport
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
        public void ExportObject(dynamic item)
        {
            if(_predefinedObjects.ContainsKey(Convert.ToString(item["name"])))
                return;

            if(!_alreadyAddedItems.ContainsKey(Convert.ToString(item["name"])) && !_ignoredBuiltInTypes.Contains(Convert.ToString(item["type"])))
            {
                SearchReplace sr = new SearchReplace();
                sr.RemoveProperties(_itemsToRemove, item);

                if(item["type"] == "group-with-exclusion")
                {
                    Parser parser = new ParserGroupWithExclusion();
                    parser.parse(item, this);
                }else if(item["type"] == "host")
                {
                    Parser parser = new ParserHostObject();
                    parser.parse(item, this);
                }else if(item["type"] == "network")
                {
                    Parser parser = new ParserNetwork();
                    parser.parse(item, this);
                }else if(item["type"] == "application-site")
                {
                    Parser parser = new ParserApplicationSite();
                    parser.parse(item, this);
                }
                /*else if(isDataCenterObject(item))
                {
                    Parser parser = new ParserDataCenterObject();
                    parser.parse(item);
                }*/
                else if(item["type"] == "updatable-object")
                {
                    /*
                        steps:
                            - check if updatable object is present in the predefined objects
                            - if it is not present try to add it, otherwise remove the object out of the rulebase
                            - if the object is present or could be added, use this object in the rulebase
                            - if object was removed out of the rulebase print a userfriendly error message
                    */
                    Parser parser = new ParserUpdatableObjects();
                    parser.parse(item, this);
                }

                item.Remove("uid");
                /* export object to File */
                string objectDir = _rootDir + "Objects/" + Convert.ToString(item["type"]);
                Directory.CreateDirectory(objectDir);
                FileWriter.ExportToFile(objectDir + $"/{item["type"]}.txt", item.ToString(), delemiter:"\n---\n");

                //Console.WriteLine(item["type"]);
                _alreadyAddedItems.Add(Convert.ToString(item["name"]), Convert.ToString(item["type"]));
                item["name"] = item["name"] + "_overwritten";
                item.Add("ignore-warnings", true);
                string command = "add-" + Convert.ToString(item["type"]);
                item.Remove("type");
                //item.Remove("uid");
            }

        }
    }
}