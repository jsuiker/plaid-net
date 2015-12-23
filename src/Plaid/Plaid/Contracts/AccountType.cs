using System.Runtime.Serialization;

namespace Plaid.Contracts
{
    [DataContract]
    public enum AccountType
    {
        Other,
        Depository,
        Credit,
        Loan,
        Mortgage,
        Brokerage
    }
}