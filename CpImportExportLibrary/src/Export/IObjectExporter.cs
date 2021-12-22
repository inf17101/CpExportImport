using CpImportExportLibrary.src.ApiOperations;
using System.Collections.Generic;

namespace CpImportExportLibrary.src.Export
{
    public interface IObjectExporter
    {
        Dictionary<string, string> _alreadyAddedItems { get; set; }
        List<string> _ignoredBuiltInTypes { get; set; }
        List<string> _itemsToRemove { get; set; }
        Dictionary<string, string> _predefinedObjects { get; set; }
        string _rootDir { get; set; }
        ISession _session { get; set; }
        void ExportObject(dynamic item);
    }
}