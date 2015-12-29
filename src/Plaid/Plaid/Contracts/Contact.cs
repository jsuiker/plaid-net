using System.Runtime.Serialization;
using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Contact<T>
    {
        [DataMember(Name = "primary", EmitDefaultValue = false)]
        public bool? Primary { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }

    [DataContract]
    public class Phone : Contact<string> { }

    [DataContract]
    public class Email : Contact<string> { }

    [DataContract]
    [JsonConverter(typeof(AddressJsonConverter))]
    public class Address : Contact<AddressEntry> { }
}