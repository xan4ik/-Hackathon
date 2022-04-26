using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CommandDLL
{
    public class EmailSender : IEmailSender
    {
        private MailAddress senderAddress;
        private SmtpClient smtpClient;

        public EmailSender()
        {
            senderAddress = new MailAddress("hackathon_dubna@mail.ru", "InfoDetector");
            smtpClient = new SmtpClient("smtp.mail.ru", 587)
            {
                Credentials = new NetworkCredential("hackathon_dubna@mail.ru", "0LsvgkxgpB6pmZiwJ4ae"),
                EnableSsl = true
            };
        }

        public async Task SendDocumentAsync(string email, IDocument document)
        {
            using (var stream = new MemoryStream(document.GetDocumntContent()))
            {
                var letter = CreatreDocumentMessage(email, stream);
                await smtpClient.SendMailAsync(letter);
            }
        }

        public async Task SendMessageAsync(string email, string message)
        {
            var letter = CreatreStringMessage(email, message);
            await smtpClient.SendMailAsync(letter);
        }


        private MailMessage CreatreDocumentMessage(string email, Stream document)
        {
            if (!IsValidEmailAddress(email))
            {
                throw new Exception("Invalid email :" + email);
            }

            var reciverAddress = new MailAddress(email);

            var letter = new MailMessage(senderAddress, reciverAddress);
                letter.Subject = "НПП Детектор";
                letter.Attachments.Add(new Attachment(document, new ContentType(MediaTypeNames.Application.Pdf)));

            return letter;            
        }


        private MailMessage CreatreStringMessage(string email, string message) 
        {
            if (!IsValidEmailAddress(email))
            {
                throw new Exception("Invalid email :" + email);
            }

            var reciverAddress = new MailAddress(email);

            return new MailMessage(senderAddress, reciverAddress)
            {
                Subject = "НПП Детектор",
                Body = message
            };
        }

        private bool IsValidEmailAddress(string address)
        {
            return !string.IsNullOrEmpty(address) && new EmailAddressAttribute().IsValid(address);
        }
    }

}

