using Domain;
using WebAPI.Tools;

namespace WebAPI.DataProviders.ProvidersCSV
{
    public class TimeEntityCsvProvider : CsvProvider<TimeEntity>
    {
        public TimeEntityCsvProvider() : base(@"wwwroot\time.csv")
        { }

        protected override TimeEntity ParseLine(string line)
        {
            var items = line.Split(';');

            var item = new TimeEntity();
                item.Id = items[0].GetIntOrDefault();
                item.Organization = items[1];
                item.IonName = items[2];
                item.SessionBegin = items[3].GetDateTimeOrDefault();
                item.SessionEnd = items[4].GetDateTimeOrDefault();
                item.ExposureSpan = items[5].GetTimeSpanOrDefault();
                item.TotalTime = items[6].GetTimeSpanOrDefault();
                item.TimeWithTB = items[7].GetTimeSpanOrDefault();
                item.TbStart = items[8].GetDateTimeOrDefault();
                item.TbEnd = items[9].GetDateTimeOrDefault();
                item.TbSpan = items[10].GetTimeSpanOrDefault();
            return item;
        }
    }

}
