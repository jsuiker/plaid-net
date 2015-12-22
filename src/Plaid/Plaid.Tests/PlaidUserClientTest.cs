using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plaid.Contracts;
using System.Threading.Tasks;

namespace Plaid.Tests
{
    [TestClass]
    public class PlaidUserClientTest
    {
        private readonly IPlaidUserClient _userClient;
        
        public PlaidUserClientTest()
        {
            _userClient = new FakePlaidUserClient();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_MissingClientId()
        {
            var client = new PlaidUserClient(null, "test", "test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_MissingSecret()
        {
            var client = new PlaidUserClient("test", null, "test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Throws_MissingEnv()
        {
            var client = new PlaidUserClient("test", "test", null);
        }

        [TestMethod]
        public async Task AddUser_Returns_OK()
        {
            var result = await _userClient.AddUser("info", "citi", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task AddUser_WithOptions_Returns_OK()
        {
            var result = await _userClient.AddUser("info", "citi", new Credentials { Username = "plaid_test", Password = "plaid_good" }, new Options());

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public async Task AddUser_Throws_MissingCredentials()
        {
            await _userClient.AddUser("test", "test", null, null);
        }

        [TestMethod]
        public async Task AddUser_Returns_Mfa_Questions()
        {
            var result = await _userClient.AddUser("info", "questions", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.Mfa);
            Assert.IsTrue(result.Data.MfaType == MfaType.Questions);
            Assert.IsNull(result.Error);
        }
        
    }
}
