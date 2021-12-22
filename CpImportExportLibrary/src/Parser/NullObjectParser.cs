using CpImportExportLibrary.src.Export;

namespace CpImportExportLibrary.src.Parser
{
    public class NullObjectParser : IParser
    {
        public void Parse(dynamic item, IObjectExporter exporter)
        {
        }
    }
}
