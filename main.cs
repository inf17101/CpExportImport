using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

/* mcs /reference:Newtonsoft.Json.dll *.cs -r:System.Net.Http /out:programm.exe */
namespace CpExportImport
{
    class Program
    {
        public static bool isDataCenterObject(dynamic item)
        {
            foreach(var key in item.Properties())
            {
                if(Convert.ToString(key).Contains("data-center"))
                    return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            //base directory for exported items
            string rootDir = "/root/ApiHttpRequests/output/";
            // read in all predefined objects of check point
            var predefinedObjects = PredefinedObjectsReader.ReadPredefinedObjects("objects_R81.csv", ";");

            // parser all data and make it ready for export
            string ip = "1.16.5.172";
            string port = "443";
            Session session = new Session(ip, port, "oklapper", "hallo123");
            //session.Login();
            //RuleBaseExporter.ExportRulebase(10, "Network", "/root/ApiHttpRequests/output", session);
            //session.Logout();
            
            JsonFileReader reader = new JsonFileReader();
            string jsonFromFile = reader.ReadContent("/root/ApiHttpRequests/output/out0.json");
            dynamic jsonResponse = JObject.Parse(jsonFromFile);
            
            SearchReplace sr = new SearchReplace();
            //sr.ReplaceAllUidsInAccessRulesByName(jsonResponse["rulebase"], jsonResponse["objects-dictionary"]);
            List<string> itemsToRemove = new List<string>() {"uid", "tags","domain", "icon", "meta-info", "read-only", "install-on", "custom-fields", "time"};
            List<string> ignoredBuiltInTypes = new List<string>() { "RulebaseAction", "CpmiAnyObject", "Track", "Global", "CpmiClusterMember", "simple-cluster", "checkpoint-host", "simple-gateway" }; 
            List<string> alreadyAddedItems = new List<string>();

            int counter = 0;
            foreach(dynamic item in jsonResponse["objects-dictionary"])
            {
                if(predefinedObjects.ContainsKey(Convert.ToString(item["name"])))
                    continue;

                if(!alreadyAddedItems.Contains(Convert.ToString(item["name"])) && !ignoredBuiltInTypes.Contains(Convert.ToString(item["type"])))
                {
                    sr.RemoveProperties(itemsToRemove, item);
                    Directory.CreateDirectory(rootDir + Convert.ToString(item["type"]));
                    if(item["type"] == "group-with-exclusion")
                    {
                        Parser parser = new ParserGroupWithExclusion();
                        parser.parse(item);
                    }else if(item["type"] == "host")
                    {
                        Parser parser = new ParserHostObject();
                        parser.parse(item);
                    }else if(item["type"] == "network")
                    {
                        Parser parser = new ParserNetwork();
                        parser.parse(item);
                    }else if(item["type"] == "application-site")
                    {
                        Parser parser = new ParserApplicationSite();
                        parser.parse(item);
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
                        parser.parse(item);
                    }

                    Console.WriteLine(item["type"]);
                    alreadyAddedItems.Add(Convert.ToString(item["name"]));
                    item["name"] = item["name"] + "_overwritten";
                    item.Add("ignore-warnings", true);
                    string command = "add-" + Convert.ToString(item["type"]);
                    item.Remove("type");
                    item.Remove("uid");
                    /*if(item.ContainsKey("cluster-members"))
                    {
                        item["members"] = item["cluster-members"];
                        item.Remove("cluster-members");
                    }*/

                    /*
                    Console.WriteLine("Object Type: " + command);
                    dynamic res = session.ApiCall(command, Convert.ToString(item));
                    if(res.ContainsKey("message"))
                    {
                        Console.WriteLine(res);
                        Console.WriteLine(item);
                    }
                    */
                    
                    counter++;
                }
            }
            //session.Publish();
            //session.Logout();
            Console.WriteLine(counter);

            //JsonFileWriter writer = new JsonFileWriter();
            //writer.Json2File("/root/ApiHttpRequests/output/out.json", Convert.ToString(jresponse2));
            /*JsonFileReader reader = new JsonFileReader();
            string jsonFromFile = reader.ReadContent("/root/ApiHttpRequests/output/out0.json");
            AccessRuleBase deserializedAccessRuleBase = JsonConvert.DeserializeObject<AccessRuleBase>(Convert.ToString(jsonFromFile));
            foreach(var item in deserializedAccessRuleBase.rulebase[0].source)
                Console.WriteLine(item);
            var jsonToWrite = JsonConvert.SerializeObject(deserializedAccessRuleBase, Formatting.Indented);*/
            //Console.WriteLine(jsonToWrite);
        }
    }
}
