using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public enum AccountSubType
    {
        [EnumMember(Value = "checking")]
        Checking,
        [EnumMember(Value = "savings")]
        Savings
    }
}