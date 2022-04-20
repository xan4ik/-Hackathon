using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataProviders;
using Domain;

namespace WebApi.Services
{
    public interface ITimeEntityService 
    {
        
    }

    public struct TotalTimeByIon 
    {
        public string IonName;
        public TimeSpan TotalTime;
    }

    public struct SessionReport 
    {
        public int SessionNumber;
        public string Status;
    }

    //TODO: проверки на null и прочее
    public class TimeEntityService : ITimeEntityService
    {
        private IDataProvider<TimeEntity> _provider; 
        public TimeEntityService(IDataProvider<TimeEntity> provider)
        {
            _provider = provider;
        }

        public IEnumerable<TotalTimeByIon> CountTotalTimeByIon()
        {
            var data = _provider.GetData();

            return data.GroupBy(x => x.IonName)
                        .Select(x => new TotalTimeByIon(){ 
                            IonName = x.Key,
                            TotalTime = new TimeSpan(x.Sum(v => v.TotalTime.Ticks))
                        });
        }


        public SessionReport GetSessionReport(int sessionNumber) 
        {
            string GetSessionStatus(TimeEntity entity) 
            {
                if (entity.SessionBegin < DateTime.Now)
                    return "Сеанс ожидает начала";
                else if (DateTime.Now >= entity.SessionBegin && DateTime.Now <= entity.SessionEnd)
                    return "Сеанс в процессе выполнения";
                else if (DateTime.Now >= entity.SessionEnd)
                    return "Сеанс завершился";
                return "Состояния сеанса не установленно";
            }

            var session = _provider.GetData().First(x => x.Id == sessionNumber);
            return new SessionReport()
            {
                SessionNumber = sessionNumber,
                Status = GetSessionStatus(session)
            }; 
        }

        public DateTime GetSessionBegin(int sessionNumber) 
        {
            var session = _provider.GetData().First(x => x.Id == sessionNumber);
            return session.SessionBegin;
        }
    }
}
