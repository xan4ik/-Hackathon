using System.Net.Http;
using System.Threading.Tasks;

namespace Command
{
    public interface IGetCommand<T, R> : ICommand 
    {
        Task<R> Execute(T args, HttpClient client);
    }
}
