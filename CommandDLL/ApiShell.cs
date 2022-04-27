using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.DTO;
using Domain.DocumentDTOs;
using System.Threading.Tasks;
using CommandDLL.DTO;

namespace CommandDLL
{
    public static class Constants 
    {
        public const string BaseURL = @"https://localhost:44355";
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
        public static string StandartDocumentURL => BaseURL + "/api/DocumentData/standart";
        public static string NonStandartDocumentURL => BaseURL + "/api/DocumentData/nonstandart";
        public static string AllowanceDocumentURL => BaseURL + "/api/DocumentData/allowance";
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
         
            var command = container.GetCommand<IAuthenticateCommand>("auth");
            await command.SignInAsync(profile, client);
            isSignIn = true;
        }

        public async Task TrySignOutAsync() 
        { 
            if (!isSignIn)
            {
                throw new Exception("You are not authorized");
            }

            var command = container.GetCommand<IAuthenticateCommand>("auth");
            await command.SignOutAsync(client);
            isSignIn = false;
        }


        public async Task<IEnumerable<TotalIonTimeUsing>> GetIonTotalTimeUsingAsync() 
        {
            var command = container.GetCommand<IGetCommand<IEnumerable<TotalIonTimeUsing>>>("ion_time");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IEnumerable<IonShortInfo>> GetIonShortInfoAsync() 
        {
            var command = container.GetCommand<IGetCommand<IEnumerable<IonShortInfo>>>("ion_info");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IonShortInfo> GetIonShortInfoAsync(string ionName) 
        {
            var command = container.GetCommand<IGetCommand<string, IonShortInfo>>("ion_info");
            var response = await command.Execute(ionName, client);

            return response;
        }


        public async Task<SessionReport> GetSessionReportAsync(int sessionNumber) 
        {
            var command = container.GetCommand<IGetCommand<int, SessionReport>>("session_report");
            var response = await command.Execute(sessionNumber, client);

            return response;
        }


        public async Task<DateTime> GetSessionBeginAsync(int sessionNumber) 
        {
            var command = container.GetCommand<IGetCommand<int, DateTime>>("session_begin");
            var response = await command.Execute(sessionNumber, client);

            return response;
        }

        public async Task<TimeSpan> GetTotalTbAsync()
        {
            var command = container.GetCommand<IGetCommand<TimeSpan>>("total_tb");
            var response = await command.Execute(client);

            return response;
        }


        public async Task<IEnumerable<ContractTimeWorkByIon>> GetContractsTimeworkAsync(string ion)
        {
            var command = container.GetCommand<IGetCommand<string, IEnumerable<ContractTimeWorkByIon>>>("contracts_timework");
            var response = await command.Execute(ion, client);

            return response;
        }


        public async Task<IEnumerable<ContractBegin>> GetContractsBeginsAsync()
        {
            var command = container.GetCommand<IGetCommand<IEnumerable<ContractBegin>>>("contracts_begin");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<IEnumerable<string>> GetIonNamesAsync() 
        {
            var command = container.GetCommand<IGetCommand<IEnumerable<string>>>("ion_names");
            var response = await command.Execute(client);

            return response;
        }

        public async Task<int> GetSessionCountAsync()
        {
            var command = container.GetCommand<IGetCommand<int>>("session_count");
            var response = await command.Execute(client);

            return response;
        }

        public async Task SendEmailMessage(string email, string message)
        {
            var command = container.GetCommand<IEmailSender>("email");
            await command.SendMessageAsync(email, message);
        }

        public async Task SendEmailDocument(string email, string message)
        {
            var command = container.GetCommand<IEmailSender>("email");
            await command.SendMessageAsync(email, message);
        }

        public async Task<AllowanceDocumnetDTO> GetAllowanceDocumentData(int id) 
        {
            var command = container.GetCommand<IGetCommand<int, AllowanceDocumnetDTO>>("document");
            var response = await command.Execute(id, client);

            return response;
        }

        public async Task<NonStandartDocumentDTO> GetNonStandartDocumentData(int id)
        {
            var command = container.GetCommand<IGetCommand<int, NonStandartDocumentDTO>>("document");
            var response = await command.Execute(id, client);

            return response;
        }

        public async Task<StandartDocumentDTO> GetStandartDocumentData(int id)
        {
            var command = container.GetCommand<IGetCommand<int, StandartDocumentDTO>>("document");
            var response = await command.Execute(id, client);

            return response;
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
