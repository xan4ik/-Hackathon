using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.DTO;
using System.Threading.Tasks;
using System.Linq;
using CommandDLL.Tools;

namespace CommandDLL.Commands
{
    public class IonShortInfoGetCommand : IGetCommand<IEnumerable<IonShortInfo>>, IGetCommand<string, IonShortInfo>
    {
        public async Task<IonShortInfo> Execute(string args, HttpClient client)
        {
            var result = await Execute(client);
            if (result.Any(x => x.IonName == args)) 
            {
                return result.First(x => x.IonName == args);
            }

            throw new Exception("No data found");
        }

        public async Task<IEnumerable<IonShortInfo>> Execute(HttpClient client)
        {
            var result = await client.GetDataAsync<IonShortInfo[]>(CreateHttpRequest());
            return result;
        }

        private HttpRequestMessage CreateHttpRequest()
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonInfoURl),
            };
        }
    }
}
