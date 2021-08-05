

namespace CpExportImport
{
    class ParserGroupWithExclusion : Parser
    {
        public dynamic parse(dynamic item)
        {

            if(item.ContainsKey("include"))
            {
                string includeObjectName = item["include"]["name"];
                item.Remove("include");
                item.Add("include", includeObjectName);
            }

            if(item.ContainsKey("except"))
            {
                string exceptObjectName = item["except"]["name"];
                item.Remove("except");
                item.Add("except", exceptObjectName);
            }

            return item;
        }
    }

}