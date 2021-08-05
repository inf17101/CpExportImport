namespace CpExportImport
{
    class ParserUpdatableObjects : Parser
    {
        public dynamic parse(dynamic item)
        {
            if(item.ContainsKey("name"))
                item.Remove("name");
            return item;
        }
    }
}