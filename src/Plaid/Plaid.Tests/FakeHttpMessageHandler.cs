using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plaid.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = request.Content == null ? new JObject() : JsonConvert.DeserializeObject<JToken>(await request.Content.ReadAsStringAsync());
            var url = request.RequestUri.AbsolutePath;
            var method = request.Method.Method;

            var username = body["username"]?.Value<string>();
            var password = body["password"]?.Value<string>();
            var accessToken = body["access_token"]?.Value<string>();
            var pin = body["pin"]?.Value<string>();
            var type = body["type"]?.Value<string>();

            #region Institutions
            if (url.StartsWith("/institutions"))
                switch (url)
                {
                    case "/institutions":
                        return GetResponse("institutions/GET_200.json", HttpStatusCode.OK);
                    case "/institutions/5301a93ac140de84910000e0":
                        return GetResponse("institutions/GET_5301a93ac140de84910000e0_200.json", HttpStatusCode.OK);
                    default:
                        return GetResponse("institutions/GET_404.json", HttpStatusCode.NotFound);
                }
            #endregion

            #region Categories
            if (url.StartsWith("/categories"))
                switch (url)
                {
                    case "/categories":
                        return GetResponse("categories/GET_200.json", HttpStatusCode.OK);
                    case "/categories/10000000":
                        return GetResponse("categories/GET_10000000_200.json", HttpStatusCode.OK);
                    default:
                        return GetResponse("categories/GET_404.json", HttpStatusCode.NotFound);
                }
            #endregion

            try
            {
                var content = File.ReadAllText($"FakeResponses/{url}/POST_200.json");

                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private HttpResponseMessage GetResponse(string resource, HttpStatusCode statusCode)
        {
            return new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StringContent(File.ReadAllText($"FakeResponses/{resource}"), Encoding.UTF8, "application/json")
            };
        }
    }
}
