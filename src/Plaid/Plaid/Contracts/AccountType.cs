using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public enum AccountType
    {
        [EnumMember(Value = "depository")]
        Depository
    }
}