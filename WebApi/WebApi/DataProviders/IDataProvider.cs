using System.Collections.Generic;

namespace WebApi.DataProviders
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> GetData();
    }
}
