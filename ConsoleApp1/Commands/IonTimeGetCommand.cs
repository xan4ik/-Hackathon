using System;
using System.Collections.Generic;
using System.Net.Http;
using WebApi.DTO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Command
{
    public class IonTimeGetCommand : IGetCommand<IEnumerable<TotalTimeByIon>>, IGetCommand<string, TotalTimeByIon>
    {
        public async Task<TotalTimeByIon> Execute(string args, HttpClient client)
        {
            var result = await Execute(client);

            var selection = result.Any(x => x.IonName == args);
            if (result.Any(x => x.IonName == args))
            {
                return result.First(x => x.IonName == args);
            }

            throw new Exception("No data found");
        }

        public async Task<IEnumerable<TotalTimeByIon>> Execute(HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonTimeURl),
            };

            var responce = await client.SendAsync(request);

            if (responce.StatusCode != System.Net.HttpStatusCode.OK) 
            {
                throw new Exception("some Exception");
            }

            var rawContent = await responce.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<TotalTimeByIon[]>(rawContent);
        }
    }
}
