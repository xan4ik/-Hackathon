using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandDLL
{
    public interface IEmailCommand : ICommand
    {
        Task SendDocumentAsync(string email, IDocument document);
        Task SendMessageAsync(string email, string message);
    }
}
