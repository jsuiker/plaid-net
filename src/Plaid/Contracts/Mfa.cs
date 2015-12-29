using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    [DataContract]
    public class Mfa
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "items")]
        public List<MfaItem> Items { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
