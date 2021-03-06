using Newtonsoft.Json.Linq;
using CpImportExportLibrary.src.Export;

namespace CpImportExportLibrary.src.Parser
{
    class ParserHostObject : IParser
    {
        public void Parse(dynamic item, IObjectExporter exporter)
        {
            JToken token = item.SelectToken("host-servers.web-server-config.standard-port-number", errorWhenNoMatch:false);
            if(token != null)
                item["host-servers"].Value<JObject>("web-server-config").Remove("standard-port-number");
        }
    }
}