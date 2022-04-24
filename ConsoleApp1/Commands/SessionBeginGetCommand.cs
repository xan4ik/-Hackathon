using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Command
{
    public class SessionBeginGetCommand : IGetCommand<int, DateTime>
    {
        public async Task<DateTime> Execute(int args, HttpClient client)
        {
            var url = String.Concat(Constants.SessionBeginURL, "/", args.ToString());
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            var response = await client.GetRawJsonDataAsync(request);

            return JsonSerializer.Deserialize<DateTime>(response);
        }
    }
}
