using System.Runtime.Serialization;
using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represents a user address
    /// </summary>
    [DataContract]
    public class AddressItem
    {
        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>
        /// The street.
        /// </value>
        [DataMember(Name = "street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        [DataMember(Name = "zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [DataMember(Name = "state")]
        public string State { get; set; }
    }
}