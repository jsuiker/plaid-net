using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    public class Institution
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("has_mfa")]
        public bool HasMfa { get; set; }

        [JsonProperty("credentials")]
        public Credentials Credentials { get; set; }

        [JsonProperty("products")]
        public string[] Products { get; set; }
    }
}
