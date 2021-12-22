
namespace CpImportExportLibrary.src.Parser
{
    public class ParserFactory
    {
        /// <summary>
        /// this class is an factory to create parsers depending on the api type of the object
        /// </summary>
        public static IParser CreateParser(string apiType)
        {
            if (apiType == "group-with-exclusion")
            {
                return new ParserGroupWithExclusion();
            }
            else if (apiType == "host")
            {
                return new ParserHostObject();
            }
            else if (apiType == "network")
            {
                return new ParserNetwork();
            }
            else if (apiType == "group")
            {
                return new ParserGroup();
            }
            else if (apiType == "application-site")
            {
                return new ParserApplicationSite();
            }
            
            return new NullObjectParser();
        }
    }
}