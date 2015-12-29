using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plaid.Contracts;
using Plaid.Exceptions;

namespace Plaid
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
    public abstract class PlaidClient
    {
        public static readonly string EnvironmentDevelopment = "https://tartan.plaid.com";
        public static readonly string EnvironmentProduction = "https://api.plaid.com";

        protected readonly string Environment;
        protected readonly HttpClient HttpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidClient"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <exception cref="System.ArgumentNullException">Missing Plaid Environment URL</exception>
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
        protected static async Task<T> Parse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            var error = JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync());
            error.StatusCode = response.StatusCode;

            throw new PlaidException(error);
        }
    }
}
