using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represens a choice in MFA response. Choice can be a question or device list item.
    /// </summary>
    [DataContract]
    public class MfaItem
    {
        /// <summary>
        /// Gets or sets the question text when MFA response type is "selections".
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        [DataMember(Name = "question")]
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the list of possible answers when MFA response type is "selections".
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        [DataMember(Name = "answers")]
        public List<string> Answers { get; set; }

        /// <summary>
        /// Gets or sets the MFA device type when response is "list".
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the mask of the phone or email when response is "list".
        /// </summary>
        /// <value>
        /// The mask.
        /// </value>
        [DataMember(Name = "mask")]
        public string Mask { get; set; }
    }
}