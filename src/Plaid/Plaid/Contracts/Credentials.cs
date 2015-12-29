using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represents the user credentials
    /// </summary>
    [DataContract]
    public class Credentials
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [JsonProperty("username"), DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [JsonProperty("password"), DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the pin.
        /// </summary>
        /// <value>
        /// The pin.
        /// </value>
        [JsonProperty("pin", DefaultValueHandling = DefaultValueHandling.Ignore), DataMember(Name = "pin")]
        public string Pin { get; set; }
    }
}
