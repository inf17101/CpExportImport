using System.Collections.Generic;

namespace CpExportImport
{
    public class ParserApplicationSite : Parser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
        {
            SearchReplace sr = new SearchReplace();
            dynamic newItem = sr.RemovePropertiesExcept(new List<string>(){
                "type", "name", "primary-category", "url-list", "application-signature", "tags", 
                "user-defined", "application-id", "additional-categories", "description", "icon", "risk",
                "urls-defined-as-regular-expression", "color", "comments", "details-level", "groups", "ignore-warnings", "ignore-errors" },
                item
                );
            return newItem;
        }
    }
}