using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Server.Dto;
using Server.Entities;

namespace Server.Services
{
    public class IterationService
    {
        private readonly HttpClient httpClient;

        public IterationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<SprintDto>> GetSprintIterations()
        {
            var response = await this.httpClient.GetAsync("https://dev.azure.com/planningpoker11/Planning%20Poker/_apis/work/teamsettings/iterations/");

            IterationsEntity iterations = JsonSerializer.Deserialize<IterationsEntity>(await response.Content.ReadAsStringAsync());

            return iterations.value.Select(value => this.MapToDto(value)).ToList();
        }

        private SprintDto MapToDto(ValueEntity valueEntity)
        {
            SprintDto result = new SprintDto();
            result.Name = valueEntity.name;
            result.Uri = new Uri(valueEntity.url);
            result.TimeFrame = this.MapToTimeFrame(valueEntity.attributes.timeFrame);

            return result;
        }

        private TimeFrame MapToTimeFrame(string timeFrameAsString)
        {
            if (string.IsNullOrEmpty(timeFrameAsString))
            {
                throw new ArgumentNullException(nameof(timeFrameAsString));
            }

            if (timeFrameAsString.Equals("past"))
            {
                return TimeFrame.Past;
            }
            else if (timeFrameAsString.Equals("current"))
            {
                return TimeFrame.Current;
            }
            else if (timeFrameAsString.Equals("future"))
            {
                return TimeFrame.Future;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
