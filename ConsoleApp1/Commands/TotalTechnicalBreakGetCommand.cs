using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Command
{
    public class TotalTechnicalBreakGetCommand : IGetCommand<TimeSpan>
    {
        public async Task<TimeSpan> Execute(HttpClient client)
        {
            var ticks = await client.GetDataAsync<long>(CreateHttpRequest());
            return new TimeSpan(ticks);
        }


        private HttpRequestMessage CreateHttpRequest()
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.TotalTbURL),
            };
        }
    }
}
