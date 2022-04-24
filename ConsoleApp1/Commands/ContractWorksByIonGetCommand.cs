using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApi.DTO;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

namespace Command
{
    public class ContractWorksByIonGetCommand : IGetCommand<string, IEnumerable<ContractTimeWorkByIon>>
    {
        public async Task<IEnumerable<ContractTimeWorkByIon>> Execute(string args, HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Concat(Constants.ContractWorksByIonURL, "/", args)),
            };
            var response = await client.GetRawJsonDataAsync(request);
            var result = JsonSerializer.Deserialize<ContractTimeWork[]>(response);

            return result.Select(x => new ContractTimeWorkByIon()
            {
                CompanyName = x.CompanyName,
                TotalTimeSpan = new TimeSpan(x.TotalTimeSpan)
            }) ;
            
        }
    }
}
