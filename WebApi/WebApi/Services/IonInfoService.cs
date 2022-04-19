using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataProviders;
using Domain;

namespace WebApi.Services
{
    public interface IIonInfoService 
    {
        
    }

    public class IonInfoServices : IIonInfoService
    {
        private IDataProvider<IonInfo> _provider; 
        public IonInfoServices(IDataProvider<IonInfo> provider)
        {
            _provider = provider;
        }
    }
}
