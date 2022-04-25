using System;
using System.Net.Http;
using System.Text.Json;


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

