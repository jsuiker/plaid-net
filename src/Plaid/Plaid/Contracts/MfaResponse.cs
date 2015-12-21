using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    public class MfaResponse : Response
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("type")]
        public MfaType MfaType { get; set; }

        [JsonProperty("mfa"), JsonConverter(typeof(MfaJsonConverter))]
        public Mfa MfaData { get; set; }
    }
}
