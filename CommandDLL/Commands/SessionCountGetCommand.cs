using CommandDLL.Tools;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommandDLL.Commands
{
    public class SessionCountGetCommand : IGetCommand<int>
    {
        public async Task<int> Execute(HttpClient client)
        {
            var result = await client.GetDataAsync<int>(CreateHttpRequest());
            return result;
        }

        private HttpRequestMessage CreateHttpRequest()
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.SessionCountURL)
            };
        }
    }
}
