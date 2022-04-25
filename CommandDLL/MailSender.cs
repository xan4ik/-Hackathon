using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace CommandDLL
{
    class MailSender
    {
        private List<IProtocol> docs;
        private ContentType ct;
        public MailSender(List<IProtocol> docs, ContentType ct)
        {
            this.docs = docs;
            this.ct = ct;
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
                    Attachment attach = new Attachment(ms, ct);
                    attach.ContentDisposition.FileName = doc.name;
                    msg.Attachments.Add(attach);
                }

                smtp.Send(msg);
                ms.Dispose();
            }
        }
        private bool IsValidEmailAddress(string address) => address != null && new EmailAddressAttribute().IsValid(address);
        

    }
}