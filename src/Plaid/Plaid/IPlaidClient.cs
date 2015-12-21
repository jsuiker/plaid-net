using Plaid.Contracts;
using System.Threading.Tasks;

namespace Plaid
{
    public interface IPlaidClient
    {
        Task<UserResponse> AddUser(string product, string type, Credentials credentials, Options options);
    }
}