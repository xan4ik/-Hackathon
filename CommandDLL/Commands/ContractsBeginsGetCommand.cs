using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApi.DTO;
using System.Threading.Tasks;
using System.Text.Json;

namespace CommandDLL
{
    public class ContractsBeginsGetCommand : IGetCommand<IEnumerable<ContractBegin>>
    {
        public async Task<IEnumerable<ContractBegin>> Execute(HttpClient client)
        {
            var result = await client.GetDataAsync<ContractBegin[]>(CreateHttpRequest());
            return result;
        }

        private HttpRequestMessage CreateHttpRequest()
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.ContracsBeginsURL),
            };
        }
    }
}
