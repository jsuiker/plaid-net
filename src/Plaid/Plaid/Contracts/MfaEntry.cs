using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    public class MfaEntry
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public List<string> Answers { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mask")]
        public string Mask { get; set; }
    }
}