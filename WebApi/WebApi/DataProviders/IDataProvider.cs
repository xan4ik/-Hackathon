using System.Collections.Generic;

namespace WebAPI.DataProviders
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> GetData();
    }
}
