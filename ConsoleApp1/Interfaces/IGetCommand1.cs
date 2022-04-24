using System.Net.Http;
using System.Threading.Tasks;

namespace Command
{
    public interface IGetCommand<T> : ICommand
    {
        Task<T> Execute(HttpClient client);
    }
}
