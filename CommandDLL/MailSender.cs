using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace CommandDLL
{
    class MailSender
    {
        private List<Protocol> docs;
        private IDocumentBuilder builder;
        public MailSender(IDocumentBuilder builder)
        {
            docs = new List<Protocol>();
            this.builder = builder;
        }
        public void AddDocument(ProtocolType type, int session)
        {
            switch (type)
            {
                case ProtocolType.Non_standard:
                    docs.Add(new Protocol { name = "Нестандартный протокол", body = builder.generateDocument("html/non_standard.html", session) });
                    break;
                case ProtocolType.Monitoring:
                    docs.Add(new Protocol { name = "Протокол мониторинга", body = builder.generateDocument("html/monitoring.html", session) });
                    break;
                case ProtocolType.Allowance:
                    docs.Add(new Protocol { name = "Протокол допуска", body = builder.generateDocument("html/allowance.html", session) });
                    break;
            }
        }
        public void Send(string user_email)
        {
            if (IsValidEmailAddress(user_email))
            {
                MailAddress from = new MailAddress("hackathon_dubna@mail.ru", "InfoDetector");
                MailAddress to = new MailAddress(user_email);
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = "НПП Детектор";
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("hackathon_dubna@mail.ru", "0LsvgkxgpB6pmZiwJ4ae");
                smtp.EnableSsl = true;

                MemoryStream ms = new MemoryStream();
                foreach (var doc in docs)
                {
                    ms = new MemoryStream(doc.body);
                    Attachment attach = new Attachment(ms, builder.ct);
                    attach.ContentDisposition.FileName = doc.name;
                    msg.Attachments.Add(attach);
                }

                smtp.Send(msg);
                ms.Dispose();
            }
        }
        private bool IsValidEmailAddress(string address) => address != null && new EmailAddressAttribute().IsValid(address);
        public enum ProtocolType
        {
            Non_standard,
            Monitoring,
            Allowance
        }

    }
}