using System.Net;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represents an error response
    /// </summary>
    [DataContract]
    public class Error
    {
        /// <summary>
        /// Gets or sets the error code. See Plaid API for details on standard codes.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message of this error.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message describing how to resolve this error.
        /// </summary>
        /// <value>
        /// The resolve.
        /// </value>
        [DataMember(Name = "resolve")]
        public string Resolve { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code associated with the HTTP request
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        [DataMember(Name = "statusCode")]
        public HttpStatusCode StatusCode { get; set; }
    }
}