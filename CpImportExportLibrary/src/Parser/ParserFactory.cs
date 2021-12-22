
namespace CpImportExportLibrary.src.Parser
{
    class ParserFactory
    {
        public static IParser CreateParser(dynamic item)
        {
            return new ParserHostObject();
        }
    }
}