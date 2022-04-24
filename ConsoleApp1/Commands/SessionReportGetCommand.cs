using System;
using System.Net.Http;
using WebApi.DTO;
using System.Threading.Tasks;
using System.Text.Json;

namespace Command
{
    public class SessionReportGetCommand :  IGetCommand<int, SessionReport>
    {
        public async Task<SessionReport> Execute(int args, HttpClient client)
        {
            var url = String.Concat(Constants.SessionReportURL, "/", args.ToString());
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            var response = await client.GetRawJsonDataAsync(request);

            return JsonSerializer.Deserialize<SessionReport>(response);
        }
    }
}
