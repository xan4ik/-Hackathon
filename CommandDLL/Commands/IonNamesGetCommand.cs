using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommandDLL.Tools;

namespace CommandDLL.Commands
{
    public class IonNamesGetCommand : IGetCommand<IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Execute(HttpClient client)
        {
            var result = await client.GetDataAsync<string[]>(CreateHttpRequest());
            return result;
        }

        private HttpRequestMessage CreateHttpRequest() 
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonNamesURl)
            };
        }
    }
}
