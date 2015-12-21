using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name ="resolve")]
        public string Resolve { get; set; }
    }
}