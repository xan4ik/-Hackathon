using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using WebApi.DTO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommandDLL
{
    public class AuthenticateCommand : IAuthenticateCommand
    {
        public async Task SignInAsync(UserProfile profile, HttpClient client)
        {
            var content = JsonSerializer.Serialize(profile);
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.SignInURL),
                Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var responce = await client.SendAsync(request);
            
            if (responce.StatusCode != System.Net.HttpStatusCode.OK) 
            {
                throw new Exception("Can't sign in");
            }
        }

        public async Task SignOutAsync(HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.SignOutURL)
            };

            var responce = await client.SendAsync(request);

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Can't  sign out");
            }
        }
    }
}
