using System;
using System.Net.Http;
using WebApi.DTO;
using System.Threading.Tasks;
using System.Text.Json;

namespace CommandDLL
{
    public class SessionReportGetCommand :  IGetCommand<int, SessionReport>
    {
        public async Task<SessionReport> Execute(int args, HttpClient client)
        {
            var result = await client.GetDataAsync<SessionReport>(CreateHttpRequest(args));
            return result;
        }


        private HttpRequestMessage CreateHttpRequest(int args)
        {
            var url = string.Concat(Constants.SessionReportURL, "/", args.ToString());

            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };
        }
    }
}
