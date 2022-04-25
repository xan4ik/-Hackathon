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
        private List<IProtocol> docs;
        private IDocumentBuilder builder;
        public MailSender(IDocumentBuilder builder)
        {
            docs = new List<IProtocol>();
            this.builder = builder;
        }
        public void AddDocument(ProtocolType type, int seans)
        {
            var protocol;
            switch (type)
            {
                case ProtocolType.Non_standard:
                    protocol = new NonStandardProtocol();
                    protocol.SetHtml(seans);
                    protocol.body = builder.generateDocument(protocol.html_text);
                    docs.Add(protocol);
                    break;
                case ProtocolType.Monitoring:
                    protocol = new MonitoringProtocol();
                    protocol.SetHtml(seans);
                    protocol.body = builder.generateDocument(protocol.html_text);
                    docs.Add(protocol);
                    break;
                case ProtocolType.Allowance:
                    protocol = new AllowanceProtocol();
                    protocol.SetHtml(seans);
                    protocol.body = builder.generateDocument(protocol.html_text);
                    docs.Add(protocol);
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