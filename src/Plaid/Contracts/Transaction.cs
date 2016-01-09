using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        [DataMember(Name = "_account")]
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember(Name = "_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Transaction"/> is pending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pending; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "pending")]
        public bool Pending { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [DataMember(Name = "category_id")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        [DataMember(Name = "meta")]
        public TransacationMetadata Metadata { get; set; }
    }

    [DataContract]
    public class TransacationMetadata
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }

    [DataContract]
    public class Location
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [DataMember(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        [DataMember(Name = "zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        [DataMember(Name = "coordinates")]
        public Coordinates Coordinates { get; set; }
    }

    /// <summary>
    /// Represents geo coordinates
    /// </summary>
    [DataContract]
    public class Coordinates
    {
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }
    }
}