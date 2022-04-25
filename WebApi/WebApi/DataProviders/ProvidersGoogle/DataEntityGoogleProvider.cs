using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataProviders.ProvidersGoogle
{
    public class DataEntityGoogleProvider : IDataProvider<DataEntity>
    {
        public IEnumerable<DataEntity> GetData()
        {
            return new DataEntity[]
            {
                new DataEntity(){ Id = 1},
                new DataEntity(){ Id = 1},
            };
        }
    }
}
