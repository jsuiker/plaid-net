using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    public class UserResponse : MfaResponse
    {        
        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }    
        
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }   
    }
}
