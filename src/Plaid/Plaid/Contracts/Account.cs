using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    public class Account
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_item")]
        public string Item { get; set; }

        [JsonProperty("balance")]
        public Balance Balance { get; set; }

        [JsonProperty("meta")]
        public AccountMetadata Metadata { get; set; }

        [JsonProperty("type")]
        public AccountType Type { get; set; }

        [JsonProperty("subtype")]
        public string SubType { get; set; }
    }
}