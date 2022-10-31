using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Server.Services
{
    
    public class WorkItemsObjects
    {
        public Workitemrelation[] workItemRelations { get; set; }
    }

    public class Workitemrelation
    {
        public object rel { get; set; }
        public object source { get; set; }
        public Target target { get; set; }
    }

    public class Target
    {
        public int id { get; set; }
        public string url { get; set; }
    }
    public class WorkItemsService
    {

        private readonly HttpClient httpClient;
        public WorkItemsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<string>> GetSprintwork()
        {

            var response = await this.httpClient.GetAsync("https://dev.azure.com/planningpoker11/e7e87964-2996-476c-9a2c-afc76144dfff/2b134744-3725-4460-a4af-0475512e9a10/_apis/work/teamsettings/iterations/99376db6-9161-424c-ba86-60a9721db44b/workitems");

            var iterations = JsonSerializer.Deserialize<WorkItemsObjects>(await response.Content.ReadAsStringAsync());//wywala


            foreach (var iteration in iterations.workItemRelations)
            {
                Console.WriteLine($"id: {iteration.target.id}");
            }
            return new List<string>();
        }
    }

}
