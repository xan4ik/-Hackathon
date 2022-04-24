using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Command
{
    public static class HttpRequestTools
    {
        public static async Task<string> GetRawJsonDataAsync(this HttpClient client, HttpRequestMessage request) 
        {
            var responce = await client.SendAsync(request);

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("some Exception");
            }

            return await responce.Content.ReadAsStringAsync();
        }
    }
}
