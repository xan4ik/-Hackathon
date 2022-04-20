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
        SessionReport GetSessionReport(int sessionNumber);
        DateTime GetSessionBegin(int sessionNumber);
        IEnumerable<ContractBegin> GetContractsBegin();
        IEnumerable<TotalTimeByIon> CountTotalTimeByIon();
        TimeSpan GetTBTotalTime();
        IEnumerable<ContractTimeWork> GetContractTimeWorksByIonName(string ionName);
    }


    //TODO: проверки на null и прочее
    //TODO: организация "-"
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
                if (entity.SessionBegin > DateTime.Now)
                    return "Сеанс ожидает начала";
                else if (DateTime.Now >= entity.SessionBegin && (entity.SessionEnd == new DateTime(0) || DateTime.Now <= entity.SessionEnd))
                    return "Сеанс в процессе выполнения";
                else if (entity.SessionEnd < DateTime.Now )
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

        public IEnumerable<ContractBegin> GetContractsBegin() 
        {
            var data = _provider.GetData();

            return data.GroupBy(x => x.Organization)
                       .Select(x => new ContractBegin()
                       {
                           CompanyName = x.Key,
                           WorkBegin = x.Min(v => v.SessionBegin)
                       });
        }

        public TimeSpan GetTBTotalTime()
        {
            var data = _provider.GetData();
            return new TimeSpan(data.Sum(x => x.TbSpan.Ticks));
        }

        public IEnumerable<ContractTimeWork> GetContractTimeWorksByIonName(string ionName) 
        {
            var data = _provider.GetData();
            return data.Where(x => x.IonName == ionName)
                       .GroupBy(x => x.Organization)
                       .Select(x => new ContractTimeWork()
                       {
                           CompanyName = x.Key,
                           TotalTimeSpan = new TimeSpan(x.Sum(v=>v.TimeWithTB.Ticks))
                       });
        }
    }
}
