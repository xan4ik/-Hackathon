using System;

namespace WebApi.Services
{
    public struct TotalTimeByIon 
    {
        public string IonName { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
