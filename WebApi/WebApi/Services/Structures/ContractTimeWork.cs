using System;

namespace WebApi.Services
{
    public struct ContractTimeWork 
    {
        public string CompanyName { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
    }
}
