using System.Net.Http;

namespace Plaid.Tests
{
    public class FakePlaidUserClient : PlaidUserClient
    {
        public FakePlaidUserClient()
            : base("test_id", "test_secret", EnvironmentDevelopment)
        {
        }

        protected override HttpClient GetHttpClient()
        {
            return new HttpClient(new FakeHttpMessageHandler());
        }
    }
}