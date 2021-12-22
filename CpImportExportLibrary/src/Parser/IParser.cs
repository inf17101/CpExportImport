using CpImportExportLibrary.src.Export;

namespace CpImportExportLibrary.src.Parser
{
    public interface IParser
    {
        public void Parse(dynamic item, IObjectExporter exporter);
    }
}