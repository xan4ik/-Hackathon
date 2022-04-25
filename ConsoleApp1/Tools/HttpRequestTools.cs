using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Command
{
    public static class HttpRequestTools
    {
        public static async Task<T> GetDataAsync<T>(this HttpClient client, HttpRequestMessage request)
        {
            var response = await client.GetRawJsonDataAsync(request);

            return JsonSerializer.Deserialize<T>(response);
        }


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
