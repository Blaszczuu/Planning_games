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

    public class TitleServices
    {
        private readonly HttpClient httpClient;

        public TitleServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<TitleToDto>> GetSprintTitle(WorkItemToDto workTitleuri)
        {
            var wItem= new WorkItemToDto();

            var response = await this.httpClient.GetAsync(workTitleuri.Uri.ToString());

            var workTitlesforid = JsonConvert.DeserializeObject<WorkTitle>(await response.Content.ReadAsStringAsync());//exception wywala 


            return workTitlesforid.fields.Select(fields => this.MapTitleToDTO(fields)).ToList();
        }
        private TitleToDto MapTitleToDTO(Fields fieldTitle)
        {
            TitleToDto titleItemDto = new TitleToDto();
            titleItemDto.SystemTitle =fieldTitle.SystemTitle;
            

            return titleItemDto;
        }
    }

}
