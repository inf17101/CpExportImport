using CpImportExportLibrary.src.Export;
using CpImportExportLibrary.src.Helpers;
using System.Collections.Generic;

namespace CpImportExportLibrary.src.Parser
{
    public class ParserApplicationSite : IParser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
        {
            SearchReplace sr = new SearchReplace();
            dynamic newItem = sr.RemovePropertiesExcept(new List<string>(){
                "type", "name", "primary-category", "url-list", "application-signature", "tags", 
                "user-defined", "application-id", "additional-categories", "description", "icon", "risk",
                "urls-defined-as-regular-expression", "color", "comments", "details-level", "groups", "ignore-warnings", "ignore-errors" },
                item);
            return newItem;
        }
    }
}