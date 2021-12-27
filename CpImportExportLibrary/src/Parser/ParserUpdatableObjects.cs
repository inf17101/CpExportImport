using CpImportExportLibrary.src.Export;
using CpImportExportLibrary.src.Helpers;
using System.Collections.Generic;

namespace CpImportExportLibrary.src.Parser
{
    class ParserUpdatableObjects : IParser
    {
        public void Parse(dynamic item, IObjectExporter exporter)
        {
            SearchReplace.RemovePropertiesExcept(new List<string>(){
                "type", "uri", "uid-in-updatable-objects-repository", "tags", 
                "color", "comments", "details-level", "ignore-warnings", "ignore-errors" },
                item);
        }
    }
}