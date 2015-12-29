using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Transaction
    {
        [DataMember(Name = "_account")]
        public string AccountId { get; set; }

        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "pending")]
        public bool Pending { get; set; }

        [DataMember(Name = "category_id")]
        public string CategoryId { get; set; }
    }
}