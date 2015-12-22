using System.Net;
using Newtonsoft.Json;

namespace Plaid.Contracts
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    
        public Error Error { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}