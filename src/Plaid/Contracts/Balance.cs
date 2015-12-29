using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represents an account balance
    /// </summary>
    [DataContract]
    public class Balance
    {
        /// <summary>
        /// Gets or sets the available balance for this account.
        /// </summary>
        /// <value>
        /// The available.
        /// </value>
        [DataMember(Name = "available")]
        public decimal Available { get; set; }

        /// <summary>
        /// Gets or sets the current balance for this account.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        [DataMember(Name = "current")]
        public decimal Current { get; set; }
    }
}