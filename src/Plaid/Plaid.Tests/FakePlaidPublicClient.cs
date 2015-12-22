using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Plaid.Tests
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
