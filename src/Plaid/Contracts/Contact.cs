using System.Runtime.Serialization;
using Newtonsoft.Json;
using Plaid.Serialization;

namespace Plaid.Contracts
{
    /// <summary>
    /// Represents a user contact
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public abstract class Contact<T>
    {
        /// <summary>
        /// Gets or sets wheather this contact item is primary.
        /// </summary>
        /// <value>
        /// The primary.
        /// </value>
        [DataMember(Name = "primary", EmitDefaultValue = false)]
        public bool? Primary { get; set; }

        /// <summary>
        /// Gets or sets the type of this contact (home, work, mobile).
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the data associated with this contact.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name = "data")]
        public T Data { get; set; }
    }

    /// <summary>
    /// Represents a phone contact
    /// </summary>
    [DataContract]
    public class Phone : Contact<string> { }

    /// <summary>
    /// Represents an email contact
    /// </summary>
    [DataContract]
    public class Email : Contact<string> { }

    /// <summary>
    /// Represents an address contact
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(AddressJsonConverter))]
    public class Address : Contact<AddressItem> { }
}