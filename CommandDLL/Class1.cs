using System;
using System.Net.Http;
using System.Text.Json;


namespace CommandDLL
{
    public interface IGetCommand<T> 
    {
        T GetData();
    }

    public class SampleMail
    {
        void Send()
        {
            var builder = new BuilderPDF();
            MailSender mailSender = new MailSender(builder);
            mailSender.AddDocument(MailSender.ProtocolType.Non_standard, 1);
            mailSender.AddDocument(MailSender.ProtocolType.Monitoring, 1);
            mailSender.AddDocument(MailSender.ProtocolType.Allowance, 1);
            mailSender.Send("user_mail@something.something");
        }
    }

    //public class DataPicker : IGetCommand<SessionReport>
    //{
    //    private string url;

    //    public SessionReport GetData()
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            client.GetAsync(url);
            
    //        }
    //            throw new NotImplementedException();
    //    }
    //}
}

