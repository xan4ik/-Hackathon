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
            var result = await client.GetDataAsync<DateTime>(CreateHttpRequest(args));
            return result;
        }


        private HttpRequestMessage CreateHttpRequest(int args)
        {
            var url = string.Concat(Constants.SessionBeginURL, "/", args.ToString());
           
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };
        }
    }
}
