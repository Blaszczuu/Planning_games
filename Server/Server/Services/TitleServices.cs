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

        public async Task<TitleDto> GetWorkItem(WorkItemDto workTitleuri)
        {
            var wItem= new WorkItemDto();

            var response = await this.httpClient.GetAsync(workTitleuri.Uri.ToString());

            var content = await response.Content.ReadAsStringAsync();
            var workTitlesforid = JsonConvert.DeserializeObject<WorkTitle>(await response.Content.ReadAsStringAsync());//exception wywala 

            return this.MapTitleToDTO(workTitlesforid.fields, workTitleuri.Id);
        }
        private TitleDto MapTitleToDTO(Fields fieldTitle, string id)
        {
            TitleDto titleItemDto = new TitleDto();
            titleItemDto.Id = int.Parse(id);
            titleItemDto.SystemTitle =fieldTitle.SystemTitle;
            
            return titleItemDto;
        }
    }

}
