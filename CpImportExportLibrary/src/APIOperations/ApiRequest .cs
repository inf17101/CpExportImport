using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CpImportExportLibrary.src.ApiOperations
{
    class ApiRequest
    {

        public static async Task<dynamic> Get(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                return response;
            }
        }

        public static async Task<dynamic> Post(String url, String data)
        {
            HttpClientHandler handler = new HttpClientHandler() 
            { 
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using (var client = new HttpClient(handler))
            {
                var response = await client.PostAsync(url,
                new StringContent(data, Encoding.UTF8, "application/json"));
                return response;
            }
        }

        public static async Task<dynamic> Post(string url, string data, string sid)
        {
            HttpClientHandler handler = new HttpClientHandler()
            { 
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using (var client = new HttpClient(handler))
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                content.Headers.Add("X-chkp-sid", sid);
                var response = await client.PostAsync(url, content);
                return response;
            }
        }

        public static async Task<dynamic> Put(String url, String data)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(url,
                     new StringContent(data, Encoding.UTF8, "application/json"));
                return response;
            }
        }
    }
}
