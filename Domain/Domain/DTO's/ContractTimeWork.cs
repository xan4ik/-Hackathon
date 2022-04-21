using System;

namespace Domain.DTO
{
    public struct ContractTimeWork 
    {
        public string CompanyName { get; set; }
        public TimeSpan TotalTimeSpan { get; set; }
    }
}
