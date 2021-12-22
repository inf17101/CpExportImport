using CpImportExportLibrary.src.Export;
using System;

namespace CpImportExportLibrary.src.Parser
{
    class ParserGroup : IParser
    {
        public dynamic parse(dynamic item, ObjectExporter exporter)
        {
            if(item.ContainsKey("members"))
            {
                for(int i = 0; i < item["members"].Count; ++i)
                {
                    //Session session = exporter._session;
                    //Console.WriteLine(item["members"][i]);
                    string command = "show-object";
                    string payload = $"{{ \"uid\": \"{Convert.ToString(item["members"][i])}\", \"details-level\": \"full\"}}";
                    dynamic res = exporter._session.ApiCall(command, payload);
                    res = res["object"];
                    if(res.ContainsKey("message") || res.ContainsKey("error"))
                        throw new FormatException($"Cannot request group member definition from api caused by: {res["message"].ToString()}");
                    
                    if(res.ContainsKey("name"))
                        item["members"][i] = res["name"];

                    if(res != null)
                        exporter.ExportObject(res);
                }
            }

            return item;
        }
    }

}