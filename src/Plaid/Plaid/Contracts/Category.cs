using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public CategoryType Type { get; set; }

        [JsonProperty("hierarchy")]
        public string[] Hierarchy { get; set; }
    }
}
