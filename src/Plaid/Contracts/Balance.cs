using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Balance
    {
        [DataMember(Name = "available")]
        public decimal Available { get; set; }

        [DataMember(Name = "current")]
        public decimal Current { get; set; }
    }
}