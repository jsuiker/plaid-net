using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    public class Error
    {
        public int Code { get; set; }
        
        public string Message { get; set; }
        
        public string Resolve { get; set; }
    }
}