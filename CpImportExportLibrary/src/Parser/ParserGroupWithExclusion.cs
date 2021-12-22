
using CpImportExportLibrary.src.Export;
using Newtonsoft.Json.Linq;

namespace CpImportExportLibrary.src.Parser
{
    class ParserGroupWithExclusion : IParser
    {
        public void Parse(dynamic item, IObjectExporter exporter)
        {

            if(item.ContainsKey("include"))
            {
                dynamic includeItem = item["include"].DeepClone();
                string includeObjectName = item["include"]["name"];
                item.Remove("include");
                item.Add("include", includeObjectName);
                if(includeObjectName != "Any")
                {
                    exporter.ExportObject(includeItem);
                }
            }

            if(item.ContainsKey("except"))
            {
                dynamic exceptItem = item["except"].DeepClone();
                string exceptObjectName = item["except"]["name"];
                item.Remove("except");
                item.Add("except", exceptObjectName);
                if(exceptObjectName != "Any")
                {
                    exporter.ExportObject((JObject) exceptItem);
                }
            }
        }
    }

}