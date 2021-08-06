using System.Collections.Generic;

namespace CpExportImport
{
    class ParserDataCenterObject : Parser
    {
        public dynamic parse(dynamic item)
        {   
            SearchReplace sr = new SearchReplace();
            if(item.ContainsKey("name-in-data-center"))
            {
                item.Remove("data-center-name");
                item.Add("data-center-name", item["name-in-data-center"]);
            }
            dynamic newItem = sr.RemovePropertiesExcept(new List<string>(){
                "data-center-name", "data-center-uid", "uri", "uid-in-data-center", "name", "tags", 
                "color", "comments", "details-level", "groups", "ignore-warnings", "ignore-errors" },
                item
                );
            if(item.ContainsKey("name"))
            {
                //newItem.Add("data-center-name", item["name"]);
                newItem.Remove("name");
            }
            newItem.Add("type", "data-center-object"); // change type to data center object
            return newItem;
        }
    }
}