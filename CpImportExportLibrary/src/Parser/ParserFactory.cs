namespace CpExportImport
{
    class ParserFactory
    {
        static Parser CreateParser(dynamic item)
        {
            return new ParserHostObject();
        }
    }
}