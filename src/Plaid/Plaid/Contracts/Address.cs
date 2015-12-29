using System.Runtime.Serialization;
using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class AddressEntry
    {
        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "zip")]
        public string Zip { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }
    }
}