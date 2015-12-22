using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Account
    {
        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "_item")]
        public string Item { get; set; }

        [DataMember(Name = "_user"), Obsolete("This property is depricated and shouldn't be used.", true)]
        public string User { get; set; }

        [DataMember(Name = "balance")]
        public Balance Balance { get; set; }

        [DataMember(Name = "meta")]
        public AccountMetadata Metadata { get; set; }

        [JsonProperty("type")]
        public AccountType Type { get; set; }

        [JsonProperty("subtype")]
        public AccountSubType SubType { get; set; }
    }
}