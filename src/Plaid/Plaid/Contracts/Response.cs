using Newtonsoft.Json;

namespace Plaid.Contracts
{
    public class Response
    {
        public ResponseCode ResponseCode { get; set; }

        public string Message { get; set; }
    
        public Error Error { get; set; }
    }
}