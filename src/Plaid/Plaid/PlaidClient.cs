using Plaid.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Plaid
{
    public class PlaidClient : IPlaidClient
    {
        public static readonly string ENVIRONMENT_DEVELOPMENT = "https://tartan.plaid.com";
        public static readonly string ENVIRONMENT_PRODUCTION = "https://api.plaid.com";

        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _environment;

        private readonly HttpClient _httpClient;

        public PlaidClient(string clientId, string secret, string environment)
        {
            if (clientId == null)
                throw new ArgumentNullException(nameof(clientId), "Missing Plaid Client Id");

            if (secret == null)
                throw new ArgumentNullException(nameof(secret), "Missing Plaid Secret");

            if (environment == null)
                throw new ArgumentNullException(nameof(environment), "Missing Plaid Environment URL");

            _clientId = clientId;
            _secret = secret;
            _environment = environment;

            _httpClient = GetHttpClient();
        }

        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <returns></returns>
        protected virtual HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        public async Task AddUser(string product, string type, Credentials credentials, Options options)
        {
            await Task.Yield();

            var response = await _httpClient.PostAsync(new Uri($"{_environment}/{product}"), GetContent(true, type, credentials, options));

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    break;
                case System.Net.HttpStatusCode.Created:
                    break;
                default:
                    break;
            }
        }

        #region Private
        private HttpContent GetContent(bool requiresAuthorization, params object[] parameters)
        {
            var content = new List<object>(parameters);
            if (requiresAuthorization)
                content.Add(new Authorization() { ClientId = _clientId, Secret = _secret });

            return new FormUrlEncodedContent(Helper.ExtractContent(content.ToArray()));
        }
        #endregion

        
    }
}