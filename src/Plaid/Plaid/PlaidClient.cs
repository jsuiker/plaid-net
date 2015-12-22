using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plaid.Contracts;

namespace Plaid
{
    public class PlaidClient : IPlaidClient
    {
        public static readonly string EnvironmentDevelopment = "https://tartan.plaid.com";
        public static readonly string EnvironmentProduction = "https://api.plaid.com";

        private readonly string _clientId;
        private readonly string _secret;
        private readonly string _environment;

        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidClient"/> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="environment">The environment.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Missing Plaid Client Id
        /// or
        /// Missing Plaid Secret
        /// or
        /// Missing Plaid Environment URL
        /// </exception>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
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

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="type">The type.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Missing credentials</exception>
        public async Task<Response<UserData>> AddUser(string product, string type, Credentials credentials, Options options)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials), "Missing credentials");

            dynamic body = new ExpandoObject();
            body.type = type;
            body.username = credentials.Username;
            body.password = credentials.Password;
            body.pin = credentials.Pin;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", product, body));

            return await Parse<UserData>(response);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async Task<Response<UserData>> GetUser(string product, string accessToken, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", $"{product}/get", body));

            return await Parse<UserData>(response);
        }

        /// <summary>
        /// Steps the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async Task<Response<UserData>> StepUser(string product, string accessToken, string[] mfaResponses, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.mfa = mfaResponses;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", $"{product}/step", body));

            return await Parse<UserData>(response);
        }

        /// <summary>
        /// Patches the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public async Task<Response<UserData>> PatchUser(string product, string accessToken, Credentials credentials)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.username = credentials.Username;
            body.password = credentials.Password;
            body.pin = credentials.Pin;

            var response = await _httpClient.SendAsync(AuthenticatedRequest("PATCH", product, body));

            return await Parse<UserData>(response);
        }

        /// <summary>
        /// Patches the step user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="mfaResponses">The mfa responses.</param>
        /// <returns></returns>
        public async Task<Response<UserData>> PatchStepUser(string product, string accessToken, string[] mfaResponses)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.mfa = mfaResponses;

            var response = await _httpClient.SendAsync(AuthenticatedRequest("PATCH", $"{product}/step", body));

            return await Parse<UserData>(response);
        }

        /// <summary>
        /// Patches the user options.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Missing options</exception>
        public async Task<Response<UserData>> PatchUserOptions(string product, string accessToken, Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options), "Missing options");

            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.options = await GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("PATCH", product, body));

            return await Parse<UserData>(response);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public async Task<Response> DeleteUser(string product, string accessToken)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            var response = await _httpClient.SendAsync(AuthenticatedRequest("DELETE", product, body));

            return await Parse(response);
        }

        /// <summary>
        /// Gets the balance.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        public async Task<Response<UserData>> GetBalance(string accessToken)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", "balance", body));

            return await Parse<Response>(response);
        }

        /// <summary>
        /// Upgrades the user.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="upgradeTo">The upgrade to.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async Task<Response<UserData>> UpgradeUser(string accessToken, string upgradeTo, Options options)
        {
            dynamic body = new ExpandoObject();
            body.access_token = accessToken;
            body.upgrade_to = upgradeTo;

            if (options != null)
                body.options = GetOptions(options);

            var response = await _httpClient.SendAsync(AuthenticatedRequest("POST", "upgrade", body));

            return await Parse<Response>(response);
        }

        /// <summary>
        /// Gets the institutions.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<Institution[]>> GetInstitutions()
        {
            var response = await _httpClient.SendAsync(PublicRequest("GET", "institutions"));

            return await Parse<Institution[]>(response);
        }

        /// <summary>
        /// Gets the institution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Response<Institution>> GetInstitution(string id)
        {
            var response = await _httpClient.SendAsync(PublicRequest("GET", $"institutions/{id}"));

            return await Parse<Institution>(response);
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<Category[]>> GetCategories()
        {
            var response = await _httpClient.SendAsync(PublicRequest("GET", "categories"));

            return await Parse<Category[]>(response);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Response<Category>> GetCategory(string id)
        {
            var response = await _httpClient.SendAsync(PublicRequest("GET", $"categories/{id}"));

            return await Parse<Category>(response);
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
                RequestUri = new Uri($"{_environment}/{path}"),
                Content = new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json")
            };
        }

        private HttpRequestMessage PublicRequest(string method, string path, dynamic body = null)
        {
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{_environment}/{path}")
            };

            if (body != null)
                body.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            return request;
        }

        private static dynamic GetOptions(Options options)
        {
            var settings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };

            return JsonConvert.SerializeObject(options, settings);
        }

        private static async Task<Response<T>> Parse<T>(HttpResponseMessage response)
        {
            var resp = new Response<T> { StatusCode = response.StatusCode };

            if (response.IsSuccessStatusCode)
                resp.Data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            else
                resp.Error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());

            return resp;
        }

        private static async Task<Response> Parse(HttpResponseMessage response)
        {
            var resp = new Response { StatusCode = response.StatusCode };

            if (response.IsSuccessStatusCode)
                resp.Message = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync())?.Message;
            else
                resp.Error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());

            return resp;
        }

        #endregion


    }
}