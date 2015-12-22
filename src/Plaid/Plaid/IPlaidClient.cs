using Plaid.Contracts;
using System.Threading.Tasks;

namespace Plaid
{
    public interface IPlaidClient
    {
        Task<UserResponse> AddUser(string product, string type, Credentials credentials, Options options);

        Task<UserResponse> GetUser(string product, string accessToken, Options options);
        
        Task<UserResponse> StepUser(string product, string accessToken, string[] mfaResponses, Options options);

        Task<UserResponse> PatchUser(string product, string accessToken, Credentials credentials);

        Task<UserResponse> PatchStepUser(string product, string accessToken, string[] mfaResponses);

        Task<UserResponse> PatchUserOptions(string product, string accessToken, Options options);

        Task<Response> DeleteUser(string product, string accessToken);
    }
}