using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommandDLL.Tools;

namespace CommandDLL.Commands
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
