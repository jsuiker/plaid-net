using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    [DataContract]
    public class Info
    {
        [DataMember(Name = "names")]
        public string[] Names { get; set; }

        [DataMember(Name = "emails")]
        public Email[] Emails { get; set; }

        [DataMember(Name = "phone_numbers")]
        public Phone[] Phones { get; set; }

        [DataMember(Name = "addresses")]
        public Address[] Addresses { get; set; }
    }
}
