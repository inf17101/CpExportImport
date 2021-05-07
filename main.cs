using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/* mcs /reference:Newtonsoft.Json.dll *.cs -r:System.Net.Http /out:programm.exe */
namespace CpExportImport
{
    class Program
    {
        public static void ExportRulebase(int limit, string name, Session session)
        {
            int offset = 0;
            while(true)
            {
                string payloadRulebase = $"{{\"offset\": {offset}, \"limit\": {limit}, \"name\": \"{name}\", \"details-level\": \"full\", \"use-object-dictionary\" : true}}";
                dynamic jresponse2 = session.ApiCall("show-access-rulebase", payloadRulebase);
                offset += limit;
                if(!jresponse2["rulebase"].HasValues)
                {
                    Console.WriteLine("End of rulebase.");
                    break;
                }
                Console.WriteLine(jresponse2);
            }
        }
        static void Main(string[] args)
        {
            
            string ip = "<ip>";
            string port = "443";
            Session session = new Session(ip, port, "<username>", "<password>");
            session.Login();
            ExportRulebase(1, "Network", session);
            session.Logout();

            //JsonFileWriter writer = new JsonFileWriter();
            //writer.Json2File("/root/ApiHttpRequests/output/out.json", Convert.ToString(jresponse2));
            /*JsonFileReader reader = new JsonFileReader();
            string jsonFromFile = reader.ReadContent("/root/ApiHttpRequests/output/out.json");
            AccessRuleBase deserializedAccessRuleBase = JsonConvert.DeserializeObject<AccessRuleBase>(Convert.ToString(jsonFromFile));
            foreach(var item in deserializedAccessRuleBase.rulebase[0].source)
                Console.WriteLine(item);
            var jsonToWrite = JsonConvert.SerializeObject(deserializedAccessRuleBase, Formatting.Indented);*/
            //Console.WriteLine(jsonToWrite);
        }
    }
}
