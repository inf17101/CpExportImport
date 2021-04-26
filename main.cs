using System;
using Newtonsoft.Json.Linq;

/* mcs /reference:Newtonsoft.Json.dll main.cs ApiRequest.cs Session.cs -r:System.Net.Http /out:programm.exe */
namespace CpExportImport
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "1.16.5.168";
            string port = "443";
            Session session = new Session(ip, port, "admin", "hallo123");
            session.Login();

            string payloadRulebase = "{\"offset\": 2, \"limit\": 2, \"name\": \"Network\", \"details-level\": \"standard\", \"use-object-dictionary\" : true}";
            var response = ApiRequest.Post(String.Format("https://{0}:443/web_api/show-access-rulebase", ip), payloadRulebase, session.Sid);
            string text = response.Result.Content.ReadAsStringAsync().Result;
            dynamic jresponse2 = JObject.Parse(text);
            Console.WriteLine(jresponse2["rulebase"][0]);

            session.Logout();
        }
    }
}
