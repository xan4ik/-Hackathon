using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApi.DTO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Command
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
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonInfoURl),
            };
            var response = await client.GetRawJsonDataAsync(request);

            return JsonSerializer.Deserialize<IonShortInfo[]>(response);
        }

    }
}
