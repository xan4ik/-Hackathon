using OpenHtmlToPdf;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommandDLL
{
    public class SampleMail
    {
        void Send()
        {
            var builder = new BuilderPDF();
            var messageBuilder = new MessageBuilder(builder);
            messageBuilder.AddDocument(MessageBuilder.ProtocolType.Non_standard, 1);
            messageBuilder.AddDocument(MessageBuilder.ProtocolType.Monitoring, 1);
            messageBuilder.AddDocument(MessageBuilder.ProtocolType.Allowance, 1);
            MailSender mailSender = new MailSender(messageBuilder.GetDocuments(), builder.ct);
            mailSender.Send("user_mail@something.something");
        }
    }



    public interface IDocument
    {
        string Name { get; }
        byte[] GetDocumntContent();
        void Save(string path);
    }

    public interface IDocumentBuilder
    {
        void SetDocumentName(string name);
        IDocument CreateDocument();
    }

    //public interface IAllowanceDocumentBulder 
    //{

    //}

    public abstract class DocumentBuilderPDF : IDocumentBuilder
    {
        protected string Template;
        private string documentName;

        public void LoadTemplate(string templatePath)
        {
            Template = File.ReadAllText(templatePath);
        }

        public void SetDocumentName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Invalid document name");
            }

            documentName = name;
        }

        public IDocument CreateDocument()
        {
            var documentContent = Pdf
                .From(Template)
                .OfSize(PaperSize.A4)
                .WithoutOutline()
                .WithMargins(1.25.Centimeters())
                .Portrait()
                .Comressed()
                .Content();

            return new SimpleDocument(documentName, documentContent);
        }
    }

    public class NonStandardPDFProtocolBuilder : DocumentBuilderPDF 
    {
        private const string TemplatePath = "html/non_standard.html";

    } 


    public class SimpleDocument : IDocument
    {
        private byte[] content;
        private string name;

        public SimpleDocument(string name, byte[] content)
        {
            this.content = content;
            this.name = name;
        }

        public string Name 
        {
            get 
            {
                return name;
            } 
        }

        public byte[] GetDocumntContent()
        {
            return content;
        }

        public void Save(string path)
        {
            File.WriteAllBytes(Path.Combine(path, name), content);
        }
    }


    public interface IEmailSender
    {
        Task SendDocumentAsync(string email, IDocument document);
        Task SendMessageAsync (string email, string message);
    }
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

