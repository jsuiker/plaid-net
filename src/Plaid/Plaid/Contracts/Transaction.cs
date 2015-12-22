using Newtonsoft.Json;
using System;

namespace Plaid.Contracts
{
    public class Transaction
    {
        [JsonProperty("_account")]
        public string AccountId { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pending")]
        public bool Pending { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
    }
}