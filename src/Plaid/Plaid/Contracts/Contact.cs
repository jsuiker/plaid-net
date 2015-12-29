using System.Runtime.Serialization;
using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    public class Contact<T>
    {
        [DataMember(Name = "primary", EmitDefaultValue = false)]
        public bool? Primary { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }

    public class Phone : Contact<string> { }

    public class Email : Contact<string> { }

    [JsonConverter(typeof(AddressJsonConverter))]
    public class Address : Contact<AddressEntry>
    {
        
    }
}