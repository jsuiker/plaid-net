using Newtonsoft.Json;

namespace Plaid.Contracts
{
    public class Response
    {
        [JsonIgnore]
        public ResponseCode ResponseCode { get; set; }

        [JsonIgnore]
        public Error Error { get; set; }
    }
}