using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class IterationService
    {
        private HttpClient httpClient;

        public IterationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<string>> GetSprintIterations()
        {
            var result = await this.httpClient.GetAsync("/GetSomething");





            return new List<string>();
        }
    }
}
