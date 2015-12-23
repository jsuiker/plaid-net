using System.Net.Http;

namespace Plaid.Tests.Fakes
{
    public class FakePlaidPublicClient : PlaidPublicClient
    {
        public FakePlaidPublicClient()
            : base(EnvironmentDevelopment)
        {
        }

        protected override HttpClient GetHttpClient()
        {
            return new HttpClient(new FakeHttpMessageHandler());
        }
    }
}
