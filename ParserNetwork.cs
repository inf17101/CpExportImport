using System.Collections.Generic;
namespace CpExportImport
{
    class ParserNetwork : Parser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
        {
            if(item.ContainsKey("mask-length4") && item.ContainsKey("subnet-mask"))
            {
                item.Remove("mask-length4");
            }else if(item.ContainsKey("mask-length6") && item.ContainsKey("subnet-mask"))
            {
                item.Remove("mask-length6");
            }
            return item;
        }
    }
}