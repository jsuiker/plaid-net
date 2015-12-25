using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    public class Options
    {
        [JsonProperty("webhook")]
        public string Webhook { get; set; }
        
        [JsonProperty("pending")]
        public bool Pending { get; set; }
        
        [JsonProperty("login_only")]
        public bool LoginOnly { get; set; }

        [JsonProperty("list")]
        public bool List { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("send_method")]
        public string SendMethod { get; set; }
    }
}
