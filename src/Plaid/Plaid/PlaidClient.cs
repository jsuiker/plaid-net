using Newtonsoft.Json;
using Plaid.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var client = new HttpClient();
            return client;
        }

        public async Task<UserResponse> AddUser(string product, string type, Credentials credentials, Options options)
        {
            var response = await _httpClient.PostAsync(new Uri($"{_environment}/{product}"), 
                GetContent(true, type, credentials, options));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<UserResponse> GetUser(string product, string accessToken, Options options)
        {
            var response = await _httpClient.PostAsync(new Uri($"{_environment}/{product}/get"), 
                GetContent(true, null, null, options, accessToken));

            return await GetResponse<UserResponse>(response);
        }
        
        public async Task<UserResponse> StepUser(string product, string accessToken, string[] mfaResponses, Options options)
        {
            var response = await _httpClient.PostAsync(new Uri($"{_environment}/{product}/step"), 
                GetContent(true, type: null, credentials: null, options: options, accessToken: accessToken, mfaResponses: mfaResponses));

            return await GetResponse<UserResponse>(response);
        }


        #region Private
        private HttpContent GetContent(bool requiresAuthorization, string type = null, Credentials credentials = null, Options options = null, string accessToken = null, string mfaResponse = null, string[] mfaResponses = null)
        {
            var content = new Dictionary<string, string>();
            dynamic d = new ExpandoObject();

            if (requiresAuthorization)
            {
                d.client_id = _clientId;
                d.secret = _secret;
            }

            if (credentials != null)
            {
                d.username = credentials.Username;
                d.password = credentials.Password;
                if (credentials.Pin != null)
                    d.pin = credentials.Pin;
            }

            if (type != null)
                d.type = type;

            if (accessToken != null)
                d.access_token = accessToken;

            if (options != null)
                d.options = JsonConvert.SerializeObject(options);

            if (mfaResponse != null)
                d.mfa = mfaResponse;
            else if (mfaResponses != null)
                d.mfa = mfaResponses;

            var c = new StringContent(JsonConvert.SerializeObject(d));
            c.Headers.ContentType.MediaType = "application/json";
            return c;
        }

        private async Task<T> GetResponse<T>(HttpResponseMessage response)
            where T : Response, new()
        {
            ResponseCode responseCode = (ResponseCode)response.StatusCode;

            if (!Enum.IsDefined(typeof(ResponseCode), responseCode))
                throw new PlaidException();

            T resp;

            if (response.IsSuccessStatusCode)
            {
                resp = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                resp = new T();
                resp.Error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());
            }
            resp.ResponseCode = responseCode;

            return resp;
        }

        #endregion


    }
}