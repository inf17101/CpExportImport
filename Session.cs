using System;
using Newtonsoft.Json.Linq;


namespace CpExportImport
{
    class Session
    {
        public string Sid 
        {
            get;
            private set;
        }

        public string Url 
        {
            get;
            private set;
        }

        public string Username 
        {
            get;
            private set;
        }

        public string Password 
        {
            get;
            private set;
        }


        public Session(string ip, string port, string username, string password)
        {
            Url = String.Format("https://{0}:{1}/web_api/", ip, port);
            Username = username;
            Password = password;
        }

        public void Login()
        {
            string data = String.Format("{{\"user\": \"{0}\",\"password\": \"{1}\"}}", Username, Password);
            var response = ApiRequest.Post(Url + "login", data);
            string jsonString = response.Result.Content.ReadAsStringAsync().Result;
            dynamic json = JObject.Parse(jsonString);
            Sid = Convert.ToString(json["sid"]);
        }

        public void Logout()
        {
            var response = ApiRequest.Post(Url + "logout", "{}", Sid);
            string jsonString = response.Result.Content.ReadAsStringAsync().Result;
            dynamic json = JObject.Parse(jsonString);
            Console.WriteLine(json);
        }
    }

}