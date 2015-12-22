﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Plaid.Tests
{
    public class FakePlaidClient : PlaidClient
    {
        public FakePlaidClient()
            : base("test_id", "test_secret", EnvironmentDevelopment)
        {
        }

        protected override HttpClient GetHttpClient()
        {
            return new HttpClient(new FakeHttpMessageHandler());
        }
    }
}
