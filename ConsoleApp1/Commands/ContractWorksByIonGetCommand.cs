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
            var result = await client.GetDataAsync<ContractTimeWork[]>(CreateHttpRequest(args));

            return result.Select(x => new ContractTimeWorkByIon()
            {
                CompanyName = x.CompanyName,
                TotalTimeSpan = new TimeSpan(x.TotalTimeSpan)
            }) ;
            
        }

        private HttpRequestMessage CreateHttpRequest(string args) 
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Concat(Constants.ContractWorksByIonURL, "/", args)),
            };
        }
    }
}
