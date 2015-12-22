using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plaid.Contracts;

namespace Plaid
{
    public class PlaidPublicClient : PlaidClient, IPlaidPublicClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidPublicClient"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public PlaidPublicClient(string environment) : base(environment)
        {
        }

        /// <summary>
        /// Gets the institutions.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<Institution[]>> GetInstitutions()
        {
            var response = await HttpClient.SendAsync(PublicRequest("GET", "institutions"));

            return await Parse<Institution[]>(response);
        }

        /// <summary>
        /// Gets the institution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Response<Institution>> GetInstitution(string id)
        {
            var response = await HttpClient.SendAsync(PublicRequest("GET", $"institutions/{id}"));

            return await Parse<Institution>(response);
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<Category[]>> GetCategories()
        {
            var response = await HttpClient.SendAsync(PublicRequest("GET", "categories"));

            return await Parse<Category[]>(response);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Response<Category>> GetCategory(string id)
        {
            var response = await HttpClient.SendAsync(PublicRequest("GET", $"categories/{id}"));

            return await Parse<Category>(response);
        }

        private HttpRequestMessage PublicRequest(string method, string path, dynamic body = null)
        {
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{Environment}/{path}")
            };

            if (body != null)
                body.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            return request;
        }
    }
}
