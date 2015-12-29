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
    public class Institution
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "has_mfa")]
        public bool HasMfa { get; set; }

        [DataMember(Name = "credentials")]
        public Credentials Credentials { get; set; }

        [DataMember(Name = "products")]
        public string[] Products { get; set; }
    }
}
