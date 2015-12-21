using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    public enum MfaType
    {
        NotAvailable = 0,
        Questions,
        Selections,
        Device,
        List
    }
}