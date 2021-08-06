using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CpExportImport
{
    class ParserHostObject : Parser
    {
        public dynamic parse(dynamic item)
        {
            JToken token = item.SelectToken("host-servers.web-server-config.standard-port-number", errorWhenNoMatch:false);
            if(token != null)
                item["host-servers"].Value<JObject>("web-server-config").Remove("standard-port-number");
            return item;
        }
    }
}