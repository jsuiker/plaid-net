using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    public class Mfa : List<Question>
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
