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
            var predefinedObjects = PredefinedObjectsReader.ReadPredefinedObjects("objects_R81.csv", delemiter:";");

            // parser all data and make it ready for export
            string ip = "1.16.5.172";
            string port = "443";
            Session session = new Session(ip, port, "oklapper", "hallo123");
            session.Login();
            //RuleBaseExporter.ExportRulebase(10, "Network", "/root/ApiHttpRequests/output", session);
            //session.Logout();
            
            JsonFileReader reader = new JsonFileReader();
            string jsonFromFile = reader.ReadContent("/root/ApiHttpRequests/output/out0.json");
            dynamic jsonResponse = JObject.Parse(jsonFromFile);

            //sr.ReplaceAllUidsInAccessRulesByName(jsonResponse["rulebase"], jsonResponse["objects-dictionary"]);
            List<string> itemsToRemove = new List<string>() {"uid", "tags","domain", "icon", "meta-info", "read-only", "install-on", "custom-fields", "time"};
            List<string> ignoredBuiltInTypes = new List<string>() { "RulebaseAction", "CpmiAnyObject", "Track", "Global", "CpmiClusterMember", "simple-cluster", "checkpoint-host", "simple-gateway" }; 
            //List<string> alreadyAddedItems = new List<string>();
            var alreadyAddedItems = new Dictionary<string, string>();
            ObjectExporter exporter = new ObjectExporter(
                session, predefinedObjects, 
                alreadyAddedItems, ignoredBuiltInTypes, 
                itemsToRemove, rootDir );
            
            foreach(dynamic item in jsonResponse["objects-dictionary"])
            {
                exporter.ExportObject(item);

                    /*
                    Console.WriteLine("Object Type: " + command);
                    dynamic res = session.ApiCall(command, Convert.ToString(item));
                    if(res.ContainsKey("message"))
                    {
                        Console.WriteLine(res);
                        Console.WriteLine(item);
                    }
                    */
                
            }
            //session.Publish();
            //session.Logout();
            //Console.WriteLine(counter);

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
