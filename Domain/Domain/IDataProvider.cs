using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> GetData();
    }
}
