using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    public class AddressEntry
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string State { get; set; }
    }
}