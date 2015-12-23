using System.Net.Http;

namespace Plaid.Tests.Fakes
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