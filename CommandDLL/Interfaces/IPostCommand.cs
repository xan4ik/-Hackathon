using System.Net.Http;

namespace CommandDLL
{
    public interface IPostCommand<T> 
    {
        void Execute(T data, HttpClient client);
    }
}
