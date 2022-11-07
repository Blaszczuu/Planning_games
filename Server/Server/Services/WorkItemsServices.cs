using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Dto;
using Server.Entities;

namespace Server.Services
{

    public class WorkItemsService
    {

        private readonly HttpClient httpClient;

        public WorkItemsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<WorkItemToDto>> GetSprintwork(SprintDto sprint)
        {

            var response = await this.httpClient.GetAsync(sprint.Uri.ToString() + "/workitems");

            var workItemRelationsEntity = JsonConvert.DeserializeObject<WorkItemRelationsEntity>(await response.Content.ReadAsStringAsync());


            return workItemRelationsEntity.workItemRelations.Select(targetEntity => this.MaptToDTO(targetEntity)).ToList();
        }
        private WorkItemToDto MaptToDTO(targetEntity targetd)
        {
            WorkItemToDto workItemDto = new WorkItemToDto();
            workItemDto.Id = targetd.target.ID;
            workItemDto.Uri = new Uri(targetd.target.Url);

            return workItemDto;
        }

    }


}
