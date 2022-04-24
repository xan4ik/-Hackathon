using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Command
{
    public class TotalTechnicalBreakGetCommand : IGetCommand<TimeSpan>
    {
        public async Task<TimeSpan> Execute(HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.TotalTbURL),
            };
            var response = await client.GetRawJsonDataAsync(request);
            var ticks = long.Parse(response);

            return new TimeSpan(ticks);
        }
    }
}
