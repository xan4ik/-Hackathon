﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.DTO;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mime;
using System.Text;
using CommandDLL.DTO;

namespace CommandDLL
{
    public static class Constants 
    {
        public const string BaseURL = @"https://0037-176-193-11-56.eu.ngrok.io";
        public static string SignInURL => BaseURL + "/api/Auth/sign_in";
        public static string SignOutURL => BaseURL + "/api/Auth/sign_out";
        public static string IonTimeURl => BaseURL + "/api/Time/ion_time";
        public static string IonNamesURl => BaseURL + "/api/IonInfo/";
        public static string IonInfoURl => BaseURL + "/api/IonInfo/short_info";
        public static string SessionReportURL => BaseURL + "/api/Time/session_report";
        public static string SessionBeginURL => BaseURL + "/api/Time/session_begin";
        public static string TotalTbURL => BaseURL + "/api/Time/totalTB";
        public static string ContractWorksByIonURL => BaseURL + "/api/Time/contracts_timework";
        public static string ContracsBeginsURL => BaseURL + "/api/Time/contracts_begin";
        public static string SessionCountURL => BaseURL + "/api/Time/session_count";
    }

    public class ApiShell : IDisposable
    {
        private HttpClient client;
        private CommandContainer container;
        private bool isSignIn;

        public ApiShell()
        {
            container = new CommandContainer();
            client = new HttpClient();
        }


        public async Task TrySignInAsync(UserProfile profile) 
        {
            if (string.IsNullOrEmpty(profile.Login)) 
            {
                throw new Exception("Bad info");
            }
         
            var command = container.RequareCommand<IAuthenticateCommand>("auth");
            await command.SignInAsync(profile, client);
            isSignIn = true;
        }

        public async Task TrySignOutAsync() 
        { 
            if (!isSignIn)
            {
                throw new Exception("You are not authorized");
            }

            var command = container.RequareCommand<IAuthenticateCommand>("auth");
            await command.SignOutAsync(client);
            isSignIn = false;
        }


        public async Task<IEnumerable<TotalIonTimeUsing>> GetIonTotalTimeUsingAsync() 
        {
            var command = container.RequareCommand<IGetCommand<IEnumerable<TotalIonTimeUsing>>>("ion_time");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IEnumerable<IonShortInfo>> GetIonShortInfoAsync() 
        {
            var command = container.RequareCommand<IGetCommand<IEnumerable<IonShortInfo>>>("ion_info");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IonShortInfo> GetIonShortInfoAsync(string ionName) 
        {
            var command = container.RequareCommand<IGetCommand<string, IonShortInfo>>("ion_info");
            var response = await command.Execute(ionName, client);

            return response;
        }


        public async Task<SessionReport> GetSessionReportAsync(int sessionNumber) 
        {
            var command = container.RequareCommand<IGetCommand<int, SessionReport>>("session_report");
            var response = await command.Execute(sessionNumber, client);

            return response;
        }


        public async Task<DateTime> GetSessionBeginAsync(int sessionNumber) 
        {
            var command = container.RequareCommand<IGetCommand<int, DateTime>>("session_begin");
            var response = await command.Execute(sessionNumber, client);

            return response;
        }

        public async Task<TimeSpan> GetTotalTbAsync()
        {
            var command = container.RequareCommand<IGetCommand<TimeSpan>>("total_tb");
            var response = await command.Execute(client);

            return response;
        }


        public async Task<IEnumerable<ContractTimeWorkByIon>> GetContractsTimeworkAsync(string ion)
        {
            var command = container.RequareCommand<IGetCommand<string, IEnumerable<ContractTimeWorkByIon>>>("contracts_timework");
            var response = await command.Execute(ion, client);

            return response;
        }


        public async Task<IEnumerable<ContractBegin>> GetContractsBeginsAsync()
        {
            var command = container.RequareCommand<IGetCommand<IEnumerable<ContractBegin>>>("contracts_begin");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IEnumerable<string>> GetIonNamesAsync() 
        {
            var command = container.RequareCommand<IGetCommand<IEnumerable<string>>>("ion_names");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<int> GetSessionCountAsync()
        {
            var command = container.RequareCommand<IGetCommand<int>>("session_count");
            var response = await command.Execute(client);

            return response;
        }

        public async Task SendEmailMessage(string email, string message)
        {
            var command = container.RequareCommand<IEmailSender>("email");
            await command.SendMessageAsync(email, message);
        }

        public async Task SendEmailDocument(string email, string message)
        {
            var command = container.RequareCommand<IEmailSender>("email");
            await command.SendMessageAsync(email, message);
        }

        #region Dispose
        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
        }

        ~ApiShell() 
        {
            Dispose(false);
        }

        private void Dispose(bool disposing) 
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    client.Dispose();
                }
                disposing = true;
            }
        }

        #endregion

    }
}
