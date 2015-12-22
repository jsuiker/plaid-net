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
            var body = JsonConvert.DeserializeObject<JToken>(await request.Content.ReadAsStringAsync());
            var url = request.RequestUri.AbsolutePath;

            var username = body["username"]?.Value<string>();
            var password = body["password"]?.Value<string>();
            var accessToken = body["access_token"]?.Value<string>();
            var pin = body["pin"]?.Value<string>();
            var type = body["type"]?.Value<string>();
            try
            {
                var content = File.ReadAllText("FakeResponses/info/POST-200.json");

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
    }
}
