﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Domain.DTO;
using System.Threading.Tasks;
using System.Linq;
using CommandDLL.DTO;
using CommandDLL.Tools;

namespace CommandDLL.Commands
{
    public class ContractWorksByIonGetCommand : IGetCommand<string, IEnumerable<ContractTimeWorkByIon>>
    {
        public async Task<IEnumerable<ContractTimeWorkByIon>> Execute(string args, HttpClient client)
        {
            var result = await client.GetDataAsync<ContractTimeWork[]>(CreateHttpRequest(args));

            return result.Select(x => new ContractTimeWorkByIon()
            {
                CompanyName = x.CompanyName,
                TotalTimeSpan = new TimeSpan(x.TotalTimeSpan)
            }) ;
            
        }

        private HttpRequestMessage CreateHttpRequest(string args) 
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Concat(Constants.ContractWorksByIonURL, "/", args)),
            };
        }
    }
}
