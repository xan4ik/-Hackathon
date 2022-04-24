using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using WebApi.DTO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Command
{
    public interface ICommand { }

    public interface IAuthenticateCommand : ICommand
    {
        Task SignInAsync(UserProfile profile, HttpClient client);
        Task SignOutAsync(HttpClient client);
    }

    public interface IGetCommnad<T> : ICommand
    {
        Task<T> Execute(HttpClient client);
    }

    public interface IGetCommand<T, R> : ICommand 
    {
        Task<R> Execute(T args, HttpClient client);
    }

    public interface IPostCommand<T> 
    {
        void Execute(T data, HttpClient client);
    }



    public static class Constants 
    {
        public const string BaseURL = @"https://localhost:44355";
        public static string SignInUrl => BaseURL + "/api/Auth/sign_in";
        public static string SignOutUrl => BaseURL + "/api/Auth/sign_out";
        public static string IonTimeURl => BaseURL + "/api/Time/ion_time";
        public static string IonInfoURl => BaseURL + "/api/IonInfo";
    }

    public class IonTimeGetCommand : IGetCommnad<IEnumerable<TotalTimeByIon>>, IGetCommand<string, TotalTimeByIon>
    {
        public async Task<TotalTimeByIon> Execute(string args, HttpClient client)
        {
            var result = await Execute(client);

            var selection = result.Any(x => x.IonName == args);
            if (result.Any(x => x.IonName == args))
            {
                return result.First(x => x.IonName == args);
            }

            throw new Exception("No data found");
        }

        public async Task<IEnumerable<TotalTimeByIon>> Execute(HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonTimeURl),
            };

            var responce = await client.SendAsync(request);

            if (responce.StatusCode != System.Net.HttpStatusCode.OK) 
            {
                throw new Exception("some Exception");
            }

            var rawContent = await responce.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<TotalTimeByIon[]>(rawContent);
        }
    }

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

    public class IonShortInfoGetCommand : IGetCommnad<IEnumerable<IonShortInfo>>, IGetCommand<string, IonShortInfo>
    {
        public async Task<IonShortInfo> Execute(string args, HttpClient client)
        {
            var result = await Execute(client);
            if (result.Any(x => x.IonName == args)) 
            {
                return result.First(x => x.IonName == args);
            }

            throw new Exception("No data found");
        }

        public async Task<IEnumerable<IonShortInfo>> Execute(HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.IonInfoURl),
            };
            var response = await client.GetRawJsonDataAsync(request);

            return JsonSerializer.Deserialize<IonShortInfo[]>(response);
        }

    }


    public class AuthenticateCommand : IAuthenticateCommand
    {
        public async Task SignInAsync(UserProfile profile, HttpClient client)
        {
            var content = JsonSerializer.Serialize(profile);
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Constants.SignInUrl),
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
                RequestUri = new Uri(Constants.SignOutUrl)
            };

            var responce = await client.SendAsync(request);

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Can't  sign out");
            }
        }
    }


    public class CommandFactory
    {
        private Dictionary<string, ICommand> commands;

        public CommandFactory()
        {
            commands = new Dictionary<string, ICommand>();

            commands.Add("auth", new AuthenticateCommand());
            commands.Add("ion_time", new IonTimeGetCommand());
            commands.Add("ion_info", new IonShortInfoGetCommand());
        }


        public T RequareCommand<T>(string name)
        {
            if (commands.ContainsKey(name)) 
            {
                return (T)commands[name];
            }

            throw new KeyNotFoundException(name);
        }
    }


    public struct TotalIonTimeUsing
    {
        public string IonName { get; set; }
        public TimeSpan TotalTime { get; set; }
    }


    public class CommandInvoker 
    {
        private HttpClient client;
        private CommandFactory factory;
        private bool isSignIn;

        public CommandInvoker()
        {
            factory = new CommandFactory();
            client = new HttpClient();
        }


        public async Task TrySignInAsync(UserProfile profile) 
        {
            var command = factory.RequareCommand<IAuthenticateCommand>("auth");
            if (IsValidModel(profile)) 
            {
                await command.SignInAsync(profile, client);
                isSignIn = true;
            }
            else throw new Exception("Bad info");
        }

        public async Task TrySignOutAsync() 
        {
            var command = factory.RequareCommand<IAuthenticateCommand>("auth");
            if (isSignIn)
            {
                await command.SignOutAsync(client);
                isSignIn = false;
            }
            else throw new Exception("You are not authorized");
        }


        public async Task<IEnumerable<TotalIonTimeUsing>> GetIonTotalTimeUsingAsync() 
        {
            var command = factory.RequareCommand<IGetCommnad<IEnumerable<TotalTimeByIon>>>("ion_time");
            var response = await command.Execute(client);

            return response.Select(x => new TotalIonTimeUsing()
            {
                IonName = x.IonName,
                TotalTime = new TimeSpan(x.TotalTime)
            }); 
        }

        public async Task<IEnumerable<IonShortInfo>> GetIonShortInfoAsync() 
        {
            var command = factory.RequareCommand<IGetCommnad<IEnumerable<IonShortInfo>>>("ion_info");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IonShortInfo> GetIonShortInfo(string ionName) 
        {
            var command = factory.RequareCommand<IGetCommand<string, IonShortInfo>>("ion_info");
            var responce = await command.Execute(ionName, client);

            return responce;
        }

        private bool IsValidModel(UserProfile profile) 
        {
            return profile.Login != string.Empty && profile.Login != null;
        
        }
    }
}
