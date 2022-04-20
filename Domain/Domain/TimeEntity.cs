using System;

namespace Domain
{
    public class TimeEntity 
    {
        public int Id { get; set; } // номер строки в документе
        public string Organization { get; set; }
        public string IonName { get; set; }
        public DateTime SessionBegin { get; set; }
        public DateTime SessionEnd { get; set; }
        public TimeSpan ExposureSpan { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan TimeWithTB { get; set; }
        public DateTime TbStart { get; set; }
        public DateTime TbEnd { get; set; }
        public TimeSpan TbSpan { get; set; }
    }
}
