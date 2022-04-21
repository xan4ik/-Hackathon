using System;

namespace Domain.DTO
{
    public struct TotalTimeByIon 
    {
        public string IonName { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
