using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataProviders.ProvidersGoogle
{
    public class IonTypeGoogleProvider : IDataProvider<IonInfo>
    {
        public IEnumerable<IonInfo> GetData()
        {
            return new IonInfo[1];
        }
    }
}
