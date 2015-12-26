using Plaid.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plaid
{
    /// <summary>
    /// Represents a client for the authenticated Plaid endpoints
    /// </summary>
    public interface IPlaidUserClient
    {
        /// <summary>
        /// Adds a user to the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="type">The type.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> AddUser(string product, string type, Credentials credentials, Options options);

        /// <summary>
        /// Gets the account and transaction data for the given token.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> GetUser(string product, string accessToken, Options options);

        /// <summary>
        /// Performs a step in multi-factor authentication.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> StepUser(string product, string accessToken, string[] mfaResponses, Options options);

        /// <summary>
        /// Updates user credentials for the given token.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        Task<Response<UserData>> PatchUser(string product, string accessToken, Credentials credentials);

        /// <summary>
        /// Performs a step in multi-factor authentication during the update user operation.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <returns></returns>
        Task<Response<UserData>> PatchStepUser(string product, string accessToken, string[] mfaResponses);

        /// <summary>
        /// Deletes a user from the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        Task<Response> DeleteUser(string product, string accessToken);

        /// <summary>
        /// Gets the user transactions.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        Task<Response<UserData>> GetBalance(string accessToken);

        /// <summary>
        /// Upgrades a user to the specified product.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="upgradeTo">The upgrade to.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        Task<Response<UserData>> UpgradeUser(string accessToken, string upgradeTo, Options options);

        /// <summary>
        /// Exchanges a public token for the given account id.
        /// </summary>
        /// <param name="publicToken">The public token.</param>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        Task<Response<string>> ExchangeToken(string publicToken, string accountId);
    }
}