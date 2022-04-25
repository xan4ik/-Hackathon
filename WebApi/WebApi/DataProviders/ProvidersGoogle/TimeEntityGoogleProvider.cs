using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataProviders.ProvidersGoogle
{
    public class TimeEntityGoogleProvider : IDataProvider<TimeEntity>
    {
        public IEnumerable<TimeEntity> GetData()
        {
            return new TimeEntity[1];
        }
    }
}
