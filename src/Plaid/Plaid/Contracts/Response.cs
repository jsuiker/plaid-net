using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Contracts
{
    [DataContract]
    public class UserResponse : Response
    {
        [JsonProperty("type")]
        private MfaType MfaType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonConverter(typeof(MfaJsonConverter)), JsonProperty("mfa")]
        public Mfa Mfa { get; set; }

        [DataMember(Name = "accounts")]
        public List<Account> Accounts { get; set; }

        [IgnoreDataMember]
        public ResponseType ResponseType { get; set; }

        
    }

    internal class MfaJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new Mfa();

            var o = JToken.Load(reader);

            if (o.Type == JTokenType.Array)
            {
                result.AddRange(o.ToObject<List<Question>>());
            }
            else if (o.Type == JTokenType.Object)
            {
                if (o["message"] != null)
                    result.Message = o["message"].Value<string>();
            }
            
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}
