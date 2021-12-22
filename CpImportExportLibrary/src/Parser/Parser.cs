using CpImportExportLibrary.src.Export;

namespace CpImportExportLibrary.src.Parser
{
    public interface IParser
    {
        dynamic parse(dynamic item, ObjectExporter exporter);
    }
}