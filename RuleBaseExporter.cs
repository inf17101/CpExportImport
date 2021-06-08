using System;
namespace CpExportImport
{
    class RuleBaseExporter
    {
        public static void ExportRulebase(int limit, string accessLayerName, string path, Session session)
        {
            int offset = 0, round = 0;
            JsonFileWriter writer = new JsonFileWriter();
            
            while(true)
            {
                string payloadRulebase = $"{{\"offset\": {offset}, \"limit\": {limit}, \"name\": \"{accessLayerName}\", \"details-level\": \"full\", \"use-object-dictionary\" : true}}";
                dynamic jresponse2 = session.ApiCall("show-access-rulebase", payloadRulebase);
                if(!jresponse2["rulebase"].HasValues)
                {
                    Console.WriteLine("End of rulebase.");
                    break;
                }

                writer.Json2File($"{path}/out{round}.json", Convert.ToString(jresponse2));
                offset += limit;
                ++round;
            }
        }
    }
}