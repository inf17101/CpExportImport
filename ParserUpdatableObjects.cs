using System.Collections.Generic;

namespace CpExportImport
{
    class ParserUpdatableObjects : Parser
    {
        public dynamic parse(dynamic item)
        {
            SearchReplace sr = new SearchReplace();
            dynamic newItem = sr.RemovePropertiesExcept(new List<string>(){
                "type", "uri", "uid-in-updatable-objects-repository", "tags", 
                "color", "comments", "details-level", "ignore-warnings", "ignore-errors" },
                item
                );
            return newItem;
        }
    }
}