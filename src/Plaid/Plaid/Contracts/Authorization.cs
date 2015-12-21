using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    [DataContract]
    public class Authorization
    {
        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }
    }
}
