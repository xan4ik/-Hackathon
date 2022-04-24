using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApi.DTO;
using System.Threading.Tasks;
using System.Text.Json;

namespace Command
{
    public class ContractsBeginsGetCommand : IGetCommand<IEnumerable<ContractBegin>>
    {
        public async Task<IEnumerable<ContractBegin>> Execute(HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.ContracsBeginsURL),
            };
            var response = await client.GetRawJsonDataAsync(request);

            return JsonSerializer.Deserialize<ContractBegin[]>(response);
        }
    }
}
