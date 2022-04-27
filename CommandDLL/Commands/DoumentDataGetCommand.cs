using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommandDLL.Tools;
using Domain.DocumentDTOs;

namespace CommandDLL.Commands
{
    public class DoumentDataGetCommand : IGetCommand<int, AllowanceDocumnetDTO>,
        IGetCommand<int, StandartDocumentDTO>,
        IGetCommand<int, NonStandartDocumentDTO>
    {
        async Task<NonStandartDocumentDTO> IGetCommand<int, NonStandartDocumentDTO>.Execute(int args, HttpClient client)
        {
            var request =new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.NonStandartDocumentURL + $"/{args}")
            };

            var result = await client.GetDataAsync<NonStandartDocumentDTO>(request);
            return result;
        }

        async Task<StandartDocumentDTO> IGetCommand<int, StandartDocumentDTO>.Execute(int args, HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.StandartDocumentURL + $"/{args}")
            };

            var result = await client.GetDataAsync<StandartDocumentDTO>(request);
            return result;
        }

        async Task<AllowanceDocumnetDTO> IGetCommand<int, AllowanceDocumnetDTO>.Execute(int args, HttpClient client)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.AllowanceDocumentURL + $"/{args}")
            };

            var result = await client.GetDataAsync<AllowanceDocumnetDTO>(request);
            return result;
        }
    }
}
