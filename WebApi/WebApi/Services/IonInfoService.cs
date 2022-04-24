using System.Collections.Generic;
using System.Linq;
using Domain;
using WebApi.DTO;


namespace WebApi.Services
{
    public interface IIonInfoService 
    {
        IEnumerable<IonShortInfo> GetIonShortInfo();
    }

    public class IonInfoServices : IIonInfoService
    {
        private IDataProvider<IonInfo> _provider; 
        public IonInfoServices(IDataProvider<IonInfo> provider)
        {
            _provider = provider;
        }

        public IEnumerable<IonShortInfo> GetIonShortInfo() 
        {
            var data = _provider.GetData();
            return data.Select(x => new IonShortInfo()
            {
                IonName = x.IonName,
                Isotope = x.Isotope,
                DistanceSI = x.DistanceSI,
                DestanceErrorSI = x.DistanceErrorSI
            });
        }
    }
}
