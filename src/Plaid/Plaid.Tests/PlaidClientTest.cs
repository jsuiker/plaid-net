using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plaid.Contracts;
using System.Threading.Tasks;

namespace Plaid.Tests
{
    [TestClass]
    public class PlaidClientTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var client = new PlaidClient("test_id", "test_secret", PlaidClient.ENVIRONMENT_DEVELOPMENT);

            var response = await client.AddUser("info", "citi", new Credentials() { Username = "plaid_selections", Password = "plaid_good" }, null);
        }
    }
}
