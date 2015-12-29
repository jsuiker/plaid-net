using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class AccountMetadata
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        [DataMember(Name = "limit")]
        public decimal? Limit { get; set; }

        /// <summary>
        /// Gets or sets the name of the official.
        /// </summary>
        /// <value>
        /// The name of the official.
        /// </value>
        [DataMember(Name = "official_name")]
        public string OfficialName { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        [DataMember(Name = "number")]
        public string Number { get; set; }
    }
}