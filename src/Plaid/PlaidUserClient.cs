using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plaid.Contracts;
using Plaid.Serialization;

namespace Plaid
{
    /// <summary>
    /// Represents a client for the authenticated Plaid endpoints
    /// </summary>
    public class PlaidUserClient : PlaidClient, IPlaidUserClient
    {
        private readonly string _clientId;
        private readonly string _secret;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidUserClient" /> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="environment">The environment.</param>
        /// <exception cref="System.ArgumentNullException">Missing Plaid Client Id
        /// or
        /// Missing Plaid Secret
        /// or
        /// Missing Plaid Environment URL</exception>
        public PlaidUserClient(string clientId, string secret, string environment)
            : base(environment)
        {
            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId), "Missing Plaid Client Id");

            if (secret == null)
                throw new ArgumentNullException(nameof(secret), "Missing Plaid Secret");

            _clientId = clientId;
            _secret = secret;
        }

        /// <summary>
        /// Adds a user to the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="type">The type.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Missing credentials</exception>
        public async Task<User> AddUser(string product, string type, Credentials credentials, Options options)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials), "Missing credentials");

            dynamic body = new ExpandoObject();
            body.type = type;
            body.credentials = credentials;
            body.options = options;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("POST", product, body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Gets the account and transaction data for the given token.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async Task<User> GetUser(string product, string accessToken, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.options = options;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("POST", $"{product}/get", body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Performs a step in multi-factor authentication.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async Task<User> StepUser(string product, string accessToken, string[] mfaResponses, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.mfa = mfaResponses;
            body.options = options;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("POST", $"{product}/step", body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Updates user credentials for the given token.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public async Task<User> PatchUser(string product, string accessToken, Credentials credentials)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.credentials = credentials;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("PATCH", product, body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Performs a step in multi-factor authentication during the update user operation.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <returns></returns>
        public async Task<User> PatchStepUser(string product, string accessToken, string[] mfaResponses)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.mfa = mfaResponses;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("PATCH", $"{product}/step", body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Deletes a user from the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public async Task DeleteUser(string product, string accessToken)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            await HttpClient.SendAsync(AuthenticatedRequest("DELETE", product, body));
        }

        /// <summary>
        /// Gets the user transactions.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public async Task<User> GetBalance(string accessToken)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("POST", "balance", body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Upgrades a user to the specified product.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="upgradeTo">The upgrade to.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async Task<User> UpgradeUser(string accessToken, string upgradeTo, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.upgrade_to = upgradeTo;
            body.options = options;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("POST", "upgrade", body));

            return await Parse<User>(response);
        }

        /// <summary>
        /// Exchanges a public token for the given account id.
        /// </summary>
        /// <param name="publicToken">The public token.</param>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<string> ExchangeToken(string publicToken, string accountId)
        {
            dynamic body = new ExpandoObject();
            body.public_token = publicToken;
            body.account_id = accountId;

            var response = await HttpClient.SendAsync(AuthenticatedRequest("POST", "exchange_token", body));

            return await base.Parse<string>((HttpResponseMessage)response);
        }

        #region Private

        private HttpRequestMessage AuthenticatedRequest(string method, string path, dynamic body)
        {
            body.client_id = _clientId;
            body.secret = _secret;

            var settings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };

            return new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{Environment}/{path}"),
                Content = new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json")
            };
        }

        /// <summary>
        /// Parses the specified response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        private new async Task<T> Parse<T>(HttpResponseMessage response)
            where T : User, new()
        {
            if (response.StatusCode != HttpStatusCode.Created)
                return await base.Parse<T>(response);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new MfaJsonConverter());

            var resp = new T
            {
                Mfa = JsonConvert.DeserializeObject<Mfa>(await response.Content.ReadAsStringAsync(), settings)
            };

            return resp;
        }

        #endregion
    }
}