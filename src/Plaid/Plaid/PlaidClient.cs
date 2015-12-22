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
            dynamic body = new ExpandoObject();
            body.type = type;
            body.username = credentials.Username;
            body.password = credentials.Password;
            body.pin = credentials.Pin;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", product, body));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<UserResponse> GetUser(string product, string accessToken, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", $"{product}/get", body));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<UserResponse> StepUser(string product, string accessToken, string[] mfaResponses, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.mfa = mfaResponses;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", $"{product}/step", body));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<UserResponse> PatchUser(string product, string accessToken, Credentials credentials)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.username = credentials.Username;
            body.password = credentials.Password;
            body.pin = credentials.Pin;

            var response = await _httpClient.SendAsync(AuthenticatedRequest("PATCH", product, body));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<UserResponse> PatchStepUser(string product, string accessToken, string[] mfaResponses)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.mfa = mfaResponses;
            
            var response = await _httpClient.SendAsync(AuthenticatedRequest("PATCH", $"{product}/step", body));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<UserResponse> PatchUserOptions(string product, string accessToken, Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options), "Missing options");

            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.options = await GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("PATCH", product, body));

            return await GetResponse<UserResponse>(response);
        }

        public async Task<Response> DeleteUser(string product, string accessToken)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            var response = await _httpClient.SendAsync(AuthenticatedRequest("DELETE", product, body));

            return await GetResponse<Response>(response);
        }


        #region Private
        private HttpRequestMessage AuthenticatedRequest(string method, string path, dynamic body)
        {
            body.client_id = _clientId;
            body.secret = _secret;

            var settings = new JsonSerializerSettings();
            settings.DefaultValueHandling = DefaultValueHandling.Ignore;

            return new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{_environment}/{path}"),
                Content = new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json")
            };
        }

        private dynamic GetOptions(Options options)
        {
            var settings = new JsonSerializerSettings();
            settings.DefaultValueHandling = DefaultValueHandling.Ignore;

            return JsonConvert.SerializeObject(options, settings);
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