using System;
using Plaid.Contracts;

namespace Plaid.Exceptions
{
    public class PlaidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public PlaidException(Error error)
        {
            Error = error;
        }

        /// <summary>
        /// Gets or sets the error response details. See Plaid API for error codes
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public Error Error { get; set; }
    }
}
