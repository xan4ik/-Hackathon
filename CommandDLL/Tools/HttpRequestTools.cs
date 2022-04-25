using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CommandDLL.Exceptions;

namespace CommandDLL.Tools
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
            try
            {
                return await TryGetRawJsonString(client, request);
            }
            catch (HttpExecutionException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw new UnhandledException(exception);
            }
        }

        private static async Task<string> TryGetRawJsonString(HttpClient client, HttpRequestMessage request)
        {
            var responce = await client.SendAsync(request);

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpExecutionException(responce.StatusCode);
            }

            return await responce.Content.ReadAsStringAsync();
        }
    }
}
