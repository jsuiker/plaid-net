using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    [DataContract]
    public class Options
    {
        [DataMember(Name = "webhook", EmitDefaultValue = false)]
        public string Webhook { get; set; }

        [DataMember(Name = "pending", EmitDefaultValue = false)]
        public bool Pending { get; set; }

        [DataMember(Name = "login_only", EmitDefaultValue = false)]
        public bool LoginOnly { get; set; }
    }
}
