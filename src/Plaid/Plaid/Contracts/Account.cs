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

        [DataMember(Name = "balance")]
        public Balance Balance { get; set; }

        [DataMember(Name = "meta")]
        public AccountMetadata Metadata { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "subtype")]
        public string SubType { get; set; }
    }
}