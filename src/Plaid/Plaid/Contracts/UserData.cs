using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    public class UserData
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("type")]
        public MfaType MfaType { get; set; }

        [JsonProperty("mfa"), JsonConverter(typeof(MfaJsonConverter))]
        public Mfa Mfa { get; set; }

        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }    
        
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }

        [DataMember(Name = "info")]
        public Info Info { get; set; }
    }
}
