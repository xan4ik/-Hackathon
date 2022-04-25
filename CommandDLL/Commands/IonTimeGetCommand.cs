using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.DTO;
using System.Threading.Tasks;
using System.Linq;
using CommandDLL.DTO;
using CommandDLL.Tools;

namespace CommandDLL.Commands
{
    public class IonTimeGetCommand : IGetCommand<IEnumerable<TotalIonTimeUsing>>, IGetCommand<string, TotalIonTimeUsing>
    {
        public async Task<TotalIonTimeUsing> Execute(string args, HttpClient client)
        {
            var result = await Execute(client);

            var selection = result.Any(x => x.IonName == args);
            if (result.Any(x => x.IonName == args))
            {
                return result.First(x => x.IonName == args);
            }

            throw new Exception("No data found");
        }

        public async Task<IEnumerable<TotalIonTimeUsing>> Execute(HttpClient client)
        {
            var result = await client.GetDataAsync<TotalTimeByIon[]>(CreateHttpRequest());
            return result.Select(x => new TotalIonTimeUsing()
            {
                IonName = x.IonName,
                TotalTime = new TimeSpan(x.TotalTime)
            });
        }

        private HttpRequestMessage CreateHttpRequest()
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonTimeURl),
            };
        }
    }
}
