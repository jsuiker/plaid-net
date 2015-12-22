using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public enum AccountType
    {
        NotAvailable = 0,
        Depository,
        Credit
    }
}