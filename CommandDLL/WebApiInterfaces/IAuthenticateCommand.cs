using System.Net.Http;
using Domain.DTO;
using System.Threading.Tasks;

namespace CommandDLL
{
    public interface IAuthenticateCommand : ICommand
    {
        Task SignInAsync(UserProfile profile, HttpClient client);
        Task SignOutAsync(HttpClient client);
    }
}
