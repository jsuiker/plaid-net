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
    [DataContract]
    public class UserData
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember( Name = "type")]
        public string MfaType { get; set; }

        [DataMember(Name = "mfa")]
        [JsonProperty("mfa"), JsonConverter(typeof(MfaJsonConverter))]
        public Mfa Mfa { get; set; }

        [DataMember(Name = "accounts")]
        public List<Account> Accounts { get; set; }    
        
        [DataMember(Name = "transactions")]
        public List<Transaction> Transactions { get; set; }

        [DataMember(Name = "info")]
        public Info Info { get; set; }
    }
}
