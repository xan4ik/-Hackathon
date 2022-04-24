using System.Net.Http;

namespace Command
{
    public interface IPostCommand<T> 
    {
        void Execute(T data, HttpClient client);
    }
}
