using System.Collections.Generic;

namespace CpExportImport
{
    public interface Parser
    {
        dynamic parse(dynamic item, ObjectExporter exporter);
    }
}