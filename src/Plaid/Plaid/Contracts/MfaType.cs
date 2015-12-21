using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public enum MfaType
    {
        [EnumMember(Value = "questions")]
        Questions,
        [EnumMember(Value = "selections")]
        Selections,
        [EnumMember(Value = "device")]
        Device
    }
}