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

    public class TimeEntityService : ITimeEntityService
    {
        private IDataProvider<TimeEntity> _provider; 
        public TimeEntityService(IDataProvider<TimeEntity> provider)
        {
            _provider = provider;
        }
    }
}
