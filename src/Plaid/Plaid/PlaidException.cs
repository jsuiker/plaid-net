using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid
{
    [DataContract]
    public class PlaidException : Exception
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "resolve")]
        public string Resolve { get; set; }
    }
}
