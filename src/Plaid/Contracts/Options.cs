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
    public class Options
    {
        [DataMember(Name = "webhook")]
        public string Webhook { get; set; }
        
        [DataMember(Name = "pending")]
        public bool Pending { get; set; }
        
        [DataMember(Name = "login_only")]
        public bool LoginOnly { get; set; }

        [DataMember(Name = "list")]
        public bool List { get; set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; set; }

        [DataMember(Name = "send_method")]
        public SendMethod SendMethod { get; set; }
    }

    [DataContract]
    public class SendMethod
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "mask")]
        public string Mask { get; set; }
    }
}
