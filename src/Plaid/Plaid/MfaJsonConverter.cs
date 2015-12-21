using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plaid.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid
{
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
                result.AddRange(o.ToObject<List<MfaEntry>>());
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
