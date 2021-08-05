using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

/* mcs /reference:Newtonsoft.Json.dll *.cs -r:System.Net.Http /out:programm.exe */
namespace CpExportImport
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "1.16.5.172";
            string port = "443";
            Session session = new Session(ip, port, "oklapper", "hallo123");
            session.Login();
            //RuleBaseExporter.ExportRulebase(10, "Network", "/root/ApiHttpRequests/output", session);
            //session.Logout();
            
            JsonFileReader reader = new JsonFileReader();
            string jsonFromFile = reader.ReadContent("/root/ApiHttpRequests/output/out2.json");
            dynamic jsonResponse = JObject.Parse(jsonFromFile);
            
            SearchReplace sr = new SearchReplace();
            //sr.ReplaceAllUidsInAccessRulesByName(jsonResponse["rulebase"], jsonResponse["objects-dictionary"]);
            List<string> itemsToRemove = new List<string>() {"uid", "tags","domain", "icon", "meta-info", "read-only", "install-on", "custom-fields", "time"};
            List<string> ignoredBuiltInTypes = new List<string>() { "RulebaseAction", "CpmiAnyObject", "Track", "Global", "CpmiClusterMember", "simple-cluster", "checkpoint-host", "simple-gateway" }; 
            List<string> alreadyAddedItems = new List<string>();
            foreach(dynamic item in jsonResponse["objects-dictionary"])
            {
                sr.RemoveProperties(itemsToRemove, item);
                if(!alreadyAddedItems.Contains(Convert.ToString(item["name"])) && !ignoredBuiltInTypes.Contains(Convert.ToString(item["type"])))
                {
                    string type = "add-" + Convert.ToString(item["type"]);
                    if(item["type"] == "group-with-exclusion")
                    {
                        Parser parser = new ParserGroupWithExclusion();
                        parser.parse(item);
                    }else if(item["type"] == "network")
                    {
                        Parser parser = new ParserNetwork();
                        parser.parse(item);
                    }else if(item["type"] == "updatable-object")
                    {
                        Parser parser = new ParserUpdatableObjects();
                        parser.parse(item);
                    }
                    
                    item.Remove("uid");
                    Console.WriteLine(item);
                    item.Remove("type");
                    /*if(item.ContainsKey("cluster-members"))
                    {
                        item["members"] = item["cluster-members"];
                        item.Remove("cluster-members");
                    }*/
                    alreadyAddedItems.Add(Convert.ToString(item["name"]));
                    item["name"] = item["name"] + "_overwritten";
                    item.Add("ignore-warnings", true);
                    dynamic res = session.ApiCall(type, Convert.ToString(item));
                    if(res.ContainsKey("message"))
                    {
                        Console.WriteLine(res);
                        Console.WriteLine(item);
                    }
                }
            }
            session.Publish();
            session.Logout();

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
