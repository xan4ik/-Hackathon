using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.DTO;
using WebAPI.DataProviders;

namespace WebAPI.Services
{
    public interface ITimeEntityService 
    {
        SessionReport GetSessionReport(int sessionNumber);
        DateTime GetSessionBegin(int sessionNumber);
        IEnumerable<ContractBegin> GetContractsBegin();
        IEnumerable<TotalTimeByIon> CountTotalTimeByIon();  
        bool HasNotSession(int sessionNumber);        
        bool HasNotIon(string ionName);        
        long GetTBTotalTime();
        IEnumerable<ContractTimeWork> GetContractTimeWorksByIonName(string ionName); 
        int GetSessionCount();
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
                            TotalTime = x.Sum(v => v.TotalTime.Ticks)
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

            if (HasNotSession(sessionNumber)) 
            {
                throw new Exception("No session with id " + sessionNumber);
            }

            var session = _provider.GetData().First(x => x.Id == sessionNumber);
            return new SessionReport()
            {
                SessionNumber = sessionNumber,
                Status = GetSessionStatus(session)
            }; 
        }

        public bool HasNotSession(int sessionNumber) 
        {
            return  !( sessionNumber >0 && _provider.GetData().Any(x => x.Id == sessionNumber));
        }

        public DateTime GetSessionBegin(int sessionNumber) 
        {
            if (HasNotSession(sessionNumber))
            {
                throw new Exception("No session with id " + sessionNumber);
            }

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

        public long GetTBTotalTime()
        {
            var data = _provider.GetData();
            return data.Sum(x => x.TbSpan.Ticks);
        }

        public bool HasNotIon(string ionName) 
        {
            return !_provider.GetData().Any(x => x.IonName == ionName);
        }

        public IEnumerable<ContractTimeWork> GetContractTimeWorksByIonName(string ionName) 
        {
            if (HasNotIon(ionName)) 
            {
                throw new Exception("No ion with name " + ionName);
            }

            var data = _provider.GetData();
            return data.Where(x => x.IonName == ionName)
                       .GroupBy(x => x.Organization)
                       .Select(x => new ContractTimeWork()
                       {
                           CompanyName = x.Key,
                           TotalTimeSpan = x.Sum(v=>v.TimeWithTB.Ticks)
                       });
        }

        public int GetSessionCount()
        {
            var data = _provider.GetData();
            return data.Max(x => x.Id);
        }
    }
}
