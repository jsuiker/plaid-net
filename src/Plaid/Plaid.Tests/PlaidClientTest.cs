using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plaid.Contracts;
using System.Threading.Tasks;

namespace Plaid.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    public class PlaidClientTest
    {
        private readonly IPlaidClient _client;

        public PlaidClientTest()
        {
            _client = new FakePlaidClient();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_MissingClientId()
        {
            new PlaidClient(null, "test", "test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_MissingSecret()
        {
            new PlaidClient("test", null, "test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_MissingEnv()
        {
            new PlaidClient("test", "test", null);
        }

        [TestMethod]
        public async Task AddUser_Returns_OK()
        {
            var result = await _client.AddUser("info", "citi", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }
    }
}
