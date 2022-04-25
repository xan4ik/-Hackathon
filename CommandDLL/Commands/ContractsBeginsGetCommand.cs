using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.DTO;
using System.Threading.Tasks;
using System.Text.Json;

namespace CommandDLL.Commands
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
