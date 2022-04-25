using System;
using System.Net;

namespace CommandDLL.Exceptions
{
    public class HttpExecutionException : Exception
    {
        public HttpExecutionException(HttpStatusCode statusCode) : base("Something wrong!")
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string GetExceptionMessage() 
        {
            switch (StatusCode) 
            {
                case (HttpStatusCode.BadRequest): 
                    {
                        return "Указаны неверные входные данные!";
                    }
                case (HttpStatusCode.NotFound): 
                    {
                        return "Необходимо авторизоваться!";
                    }
                default:
                    {
                        return "Необработанное исключение";
                    }
            }
        }
        
    }
}
