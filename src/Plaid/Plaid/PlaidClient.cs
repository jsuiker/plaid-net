using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plaid.Contracts;

namespace Plaid
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
    public abstract class PlaidClient
    {
        public static readonly string EnvironmentDevelopment = "https://tartan.plaid.com";
        public static readonly string EnvironmentProduction = "https://api.plaid.com";

        protected readonly string Environment;
        protected readonly HttpClient HttpClient;

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected PlaidClient(string environment)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment), "Missing Plaid Environment URL");

            Environment = environment;
            HttpClient = GetHttpClient();
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
        /// Parses the specified response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        protected static async Task<Response<T>> Parse<T>(HttpResponseMessage response)
        {
            var resp = new Response<T> { StatusCode = response.StatusCode };

            if (response.IsSuccessStatusCode)
                resp.Data = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            else
                resp.Error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());

            return resp;
        }

        /// <summary>
        /// Parses the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        protected static async Task<Response> Parse(HttpResponseMessage response)
        {
            var resp = new Response { StatusCode = response.StatusCode };

            if (response.IsSuccessStatusCode)
                resp.Message = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync())?.Message;
            else
                resp.Error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());

            return resp;
        }
    }
}
