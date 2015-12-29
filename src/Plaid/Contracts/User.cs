using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represent a response containing user information (accounts, transactions, mfa)
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        [DataMember(Name = "access_token", EmitDefaultValue = false)]
        public string AccessToken { get; set; }
        
        [DataMember(Name = "mfa", EmitDefaultValue = false)]
        public Mfa Mfa { get; set; }

        /// <summary>
        /// Gets or sets a list of accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        [DataMember(Name = "accounts", EmitDefaultValue = false)]
        public List<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets a list of transactions.
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        [DataMember(Name = "transactions", EmitDefaultValue = false)]
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the user personal information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        [DataMember(Name = "info", EmitDefaultValue = false)]
        public Info Info { get; set; }
    }
}
