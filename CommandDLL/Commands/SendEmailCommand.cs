using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandDLL.Commands
{
    public class SendEmailCommand : IEmailCommand
    {
        private EmailSender sender;

        public SendEmailCommand()
        {
            sender = new EmailSender();
        }

        public async Task SendDocumentAsync(string email, IDocument document)
        {
            await sender.SendDocumentAsync(email, document);
        }

        public async Task SendMessageAsync(string email, string message)
        {
            await sender.SendMessageAsync(email, message);
        }
    }
}
