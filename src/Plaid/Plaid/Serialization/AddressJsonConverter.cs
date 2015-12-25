using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plaid.Contracts;

namespace Plaid.Serialization
{
    internal class AddressJsonConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new Address();
            var jToken = JToken.Load(reader);
            result.Primary = jToken["primary"]?.Value<bool>();
            result.Type = jToken["type"]?.Value<string>();

            result.Data = jToken["data"] == null ? jToken.ToObject<AddressEntry>() : jToken["data"].ToObject<AddressEntry>();

            return result;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}