using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plaid
{
    public class Helper
    {
        public static Dictionary<string, string> ExtractContent(object[] parameters)
        {
            var dict = new Dictionary<string, string>();

            if (parameters == null)
                return dict;

            foreach (var item in parameters)
            {
                if (item == null) continue;

                foreach (var itemProperty in item.GetType().GetProperties())
                    foreach (var itemAttributes in itemProperty.CustomAttributes)
                        if (itemAttributes.AttributeType == typeof(DataMemberAttribute))
                        {
                            var key = itemProperty.Name;
                            var emit = true;
                            foreach (var attributeArguments in itemAttributes.NamedArguments)
                            {
                                if (attributeArguments.MemberName == nameof(DataMemberAttribute.Name))
                                {
                                    key = attributeArguments.TypedValue.Value.ToString();
                                }
                                if (attributeArguments.MemberName == nameof(DataMemberAttribute.EmitDefaultValue))
                                {
                                    emit = (bool)attributeArguments.TypedValue.Value;
                                }
                            }

                            if (emit)
                                dict.Add(key, itemProperty.GetValue(item).ToString());
                        }
            }

            return dict;
        }
    }
}
