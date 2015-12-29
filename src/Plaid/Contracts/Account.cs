using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represents user account information
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember(Name = "_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier that's unique for the access token. 
        /// Multiple account associated with the same user and token will share the same Item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [DataMember(Name = "_item")]
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the balance of this account.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        [DataMember(Name = "balance")]
        public Balance Balance { get; set; }

        /// <summary>
        /// Gets or sets the account metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        [DataMember(Name = "meta")]
        public AccountMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets the account type. Ex: "depository", "credit", "loan", etc.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the subtype of the account. Ex: "checking", "savings", etc.
        /// </summary>
        /// <value>
        /// The type of the sub.
        /// </value>
        [DataMember(Name = "subtype")]
        public string SubType { get; set; }
    }
}