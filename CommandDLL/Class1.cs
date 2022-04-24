using System;
using System.Net.Http;
using System.Text.Json;


namespace CommandDLL
{
    public interface IGetCommand<T> 
    {
        T GetData();
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

