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
        /// <summary>
        /// Gets or sets the user names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        [DataMember(Name = "names", EmitDefaultValue = false)]
        public string[] Names { get; set; }

        /// <summary>
        /// Gets or sets the user emails.
        /// </summary>
        /// <value>
        /// The emails.
        /// </value>
        [DataMember(Name = "emails", EmitDefaultValue = false)]
        public Email[] Emails { get; set; }

        /// <summary>
        /// Gets or sets the user phones.
        /// </summary>
        /// <value>
        /// The phones.
        /// </value>
        [DataMember(Name = "phone_numbers", EmitDefaultValue = false)]
        public Phone[] Phones { get; set; }

        /// <summary>
        /// Gets or sets the user addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public Address[] Addresses { get; set; }
    }
}
