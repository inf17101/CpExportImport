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
            string ip = "<ip>";
            string port = "443";
            Session session = new Session(ip, port, "<user>", "<password>");
            session.Login();
            //RuleBaseExporter.ExportRulebase(10, "Network", "/root/ApiHttpRequests/output", session);
            //session.Logout();
            JsonFileReader reader = new JsonFileReader();
            string jsonFromFile = reader.ReadContent("/root/ApiHttpRequests/output/out0.json");
            dynamic jsonResponse = JObject.Parse(jsonFromFile);
            SearchReplace sr = new SearchReplace();
            //sr.ReplaceAllUidsInAccessRulesByName(jsonResponse["rulebase"], jsonResponse["objects-dictionary"]);
            List<string> itemsToRemove = new List<String>() {"uid", "tags","domain", "icon", "meta-info", "read-only", "install-on", "custom-fields", "time"};
            List<string> alreadyAddedItems = new List<String>();
            foreach(dynamic item in jsonResponse["objects-dictionary"])
            {
                sr.RemoveProperties(itemsToRemove, item);
                if(!alreadyAddedItems.Contains(Convert.ToString(item["name"])))
                {
                    string type = "add-" + Convert.ToString(item["type"]);
                    item.Remove("uid");
                    item.Remove("type");
                    alreadyAddedItems.Add(Convert.ToString(item["name"]));
                    item["name"] = item["name"] + "_overwritten";
                    item.Add("ignore-warnings", true);
                    dynamic res = session.ApiCall(type, Convert.ToString(item));
                    //Console.WriteLine(res);
                    if(res.ContainsKey("errors") || (res.ContainsKey("code") && res["code"] == "generic_err_invalid_parameter_name"))
                    {
                        Console.WriteLine(res);
                        Console.WriteLine(item);
                        break;
                    }
                    //Console.WriteLine(String.Join("\n", alreadyAddedItems));
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
