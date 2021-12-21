using System;
using System.IO;
namespace CpExportImport
{
    class AccessControlPolicyExporter
    {
        private Session _session;
        private ObjectExporter _objectExporter;
        private dynamic _policyPackageInformation;
        private string _outPath;
        private static readonly int _amountOfRulesPerQuery = 10;

        public AccessControlPolicyExporter(Session session, ObjectExporter objectExporter, dynamic policyPackageInformation, string outpath)
        {
            _session = session;
            _objectExporter = objectExporter;
            _policyPackageInformation = policyPackageInformation;
            _outPath = outpath;
        }

        private void ExportLayer(string layerName, int limit)
        {
            int offset = 0, round = 0;
            JsonFileWriter writer = new JsonFileWriter();
            string layerOutPath = _outPath + layerName + "/";
            Directory.CreateDirectory(layerOutPath);
            while (true)
            {
                string payloadRulebase = $"{{\"offset\": {offset}, \"limit\": {limit}, \"name\": \"{layerName}\", \"details-level\": \"full\", \"use-object-dictionary\" : true}}";
                dynamic jresponse = _session.ApiCall("show-access-rulebase", payloadRulebase);
                if (!jresponse["rulebase"].HasValues)
                {
                    Console.WriteLine("End of rulebase.");
                    break;
                }

                SearchReplace sr = new SearchReplace();
                jresponse["rulebase"] = sr.ReplaceAllUidsInAccessRulesByName(jresponse["rulebase"], jresponse["objects-dictionary"]);

                writer.Json2File($"{layerOutPath}/out{round}.json", Convert.ToString(jresponse));
                offset += limit;
                ++round;

                foreach (dynamic item in jresponse["objects-dictionary"])
                {
                    if(Convert.ToString(item["type"]) == "access-layer")
                    {
                        /* handle special case if inline policy appears inside objects */
                        ExportLayer(Convert.ToString(item["name"]), _amountOfRulesPerQuery);
                    }else
                    {
                        _objectExporter.ExportObject(item);
                    }
                }
                    
            }
        }
        
        public void ExportRulebase()
        {
            if (!_policyPackageInformation.ContainsKey("access-layers"))
                return;
            _outPath += "/Layers/";
            Directory.CreateDirectory(_outPath);
            dynamic accessControlLayers = _policyPackageInformation["access-layers"];
            foreach(dynamic layer in accessControlLayers)
                ExportLayer(Convert.ToString(layer["name"]), _amountOfRulesPerQuery);
        }
    }
}
