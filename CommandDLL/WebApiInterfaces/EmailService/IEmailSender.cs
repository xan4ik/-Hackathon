using System.Threading.Tasks;

namespace CommandDLL
{
    public interface IEmailSender
    {
        Task SendDocumentAsync(string email, IDocument document);
        Task SendMessageAsync (string email, string message);
    }

}

