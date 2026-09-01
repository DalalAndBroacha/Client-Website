using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PieReports.Models
{
    public class HttpCall
    {
        public static string HttpPostMethod(string url,object data)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using var httpClient = new HttpClient(clientHandler);
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = httpClient.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
            //data = JsonConvert.DeserializeObject<data>(apiResponse);

        }
        public static string HttpGetMethod(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var httpClient = new HttpClient(clientHandler))
            {
                using (var response = httpClient.GetAsync(url).Result)
                {
                    return response.Content.ReadAsStringAsync().Result;
                    //data = JsonConvert.DeserializeObject<data>(apiResponse);
                }

            }
        }

    }
}
