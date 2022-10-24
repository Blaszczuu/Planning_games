using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Server.Services
{
    public class IterationService
    {
        private readonly HttpClient httpClient;
        public IterationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<string>> GetSprintIterations()
        {
            var result = await this.httpClient.GetAsync("https://dev.azure.com/planningpoker11/Planning%20Poker/_apis/work/teamsettings/iterations/");
            
            var containt = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(containt);
            JObject joResponse = JObject.Parse(containt);
            JObject ojObject = (JObject)joResponse["value"];
            //JArray array = (JArray)ojObject["value"];
            int id = Convert.ToInt32(ojObject[0].ToString());
            Console.WriteLine(id);  


            return new List<string>();
        }
    }
}
