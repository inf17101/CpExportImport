using System.Collections.Generic;

namespace CpExportImport
{
    class ParserApplicationSite : Parser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
        {
            SearchReplace sr = new SearchReplace();
            dynamic newItem = sr.RemovePropertiesExcept(new List<string>(){
                "type", "name", "primary-category", "url-list", "application-signature", "tags", 
                "additional-categories", "description", "urls-defined-as-regular-expression",
                "color", "comments", "details-level", "groups", "ignore-warnings", "ignore-errors" },
                item
                );
            return newItem;
        }
    }
}