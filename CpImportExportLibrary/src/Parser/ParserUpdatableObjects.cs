using CpImportExportLibrary.src.Export;
using CpImportExportLibrary.src.Helpers;
using System.Collections.Generic;

namespace CpImportExportLibrary.src.Parser
{
    class ParserUpdatableObjects : IParser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
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