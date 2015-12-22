using Plaid.Contracts;
using System.Threading.Tasks;

namespace Plaid
{
    public interface IPlaidClient
    {
        Task<UserResponse> AddUser(string product, string type, Credentials credentials, Options options);

        Task<UserResponse> GetUser(string product, string accessToken, Options options);
        
        Task<UserResponse> StepUser(string product, string accessToken, string[] mfaResponses, Options options);
    }
}