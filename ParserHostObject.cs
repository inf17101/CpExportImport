using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CpExportImport
{
    class ParserHostObject : Parser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
        {
            JToken token = item.SelectToken("host-servers.web-server-config.standard-port-number", errorWhenNoMatch:false);
            if(token != null)
                item["host-servers"].Value<JObject>("web-server-config").Remove("standard-port-number");
            return item;
        }
    }
}