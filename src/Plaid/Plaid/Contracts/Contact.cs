using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    public class Contact<T>
    {
        public bool? Primary { get; set; }

        public string Type { get; set; }

        public T Data { get; set; }
    }

    public class Phone : Contact<string> { }

    public class Email : Contact<string> { }

    [JsonConverter(typeof(AddressJsonConverter))]
    public class Address : Contact<AddressEntry>
    {
        
    }
}