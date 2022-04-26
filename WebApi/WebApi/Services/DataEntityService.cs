using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataProviders;
using Domain;
using Domain.DTO;

namespace WebAPI.Services
{
    public interface IDataEntityService 
    {
        
    }

    public class DataEntityService : IDataEntityService
    {
        private IDataProvider<DataEntity> _provider; 
        public DataEntityService(IDataProvider<DataEntity> provider)
        {
            _provider = provider;
        }



    }
}
