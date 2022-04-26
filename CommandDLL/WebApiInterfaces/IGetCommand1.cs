using System.Net.Http;
using System.Threading.Tasks;

namespace CommandDLL
{
    public interface IGetCommand<T> : ICommand
    {
        Task<T> Execute(HttpClient client);
    }
}
