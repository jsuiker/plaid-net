using Plaid.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plaid
{
    public interface IPlaidUserClient
    {
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="type">The type.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> AddUser(string product, string type, Credentials credentials, Options options);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> GetUser(string product, string accessToken, Options options);

        /// <summary>
        /// Steps the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> StepUser(string product, string accessToken, string[] mfaResponses, Options options);

        /// <summary>
        /// Patches the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        Task<Response<UserData>> PatchUser(string product, string accessToken, Credentials credentials);

        /// <summary>
        /// Patches the step user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <returns></returns>
        Task<Response<UserData>> PatchStepUser(string product, string accessToken, string[] mfaResponses);

        /// <summary>
        /// Patches the user options.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> PatchUserOptions(string product, string accessToken, Options options);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        Task<Response> DeleteUser(string product, string accessToken);

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        Task<Response<UserData>> GetBalance(string accessToken);

        /// <summary>
        /// Upgrades the user.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="upgradeTo">The upgrade to.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> UpgradeUser(string accessToken, string upgradeTo, Options options);
    }
}