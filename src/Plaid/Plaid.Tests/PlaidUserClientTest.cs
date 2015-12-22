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

        [TestMethod]
        public async Task AddUser_Returns_Mfa_Selections()
        {
            var result = await _userClient.AddUser("info", "selections", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.Mfa);
            Assert.IsTrue(result.Data.MfaType == MfaType.Selections);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task AddUser_Returns_Mfa_List()
        {
            var result = await _userClient.AddUser("info", "list", new Credentials { Username = "plaid_test", Password = "plaid_good" }, new Options { List = true });

            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.Mfa);
            Assert.IsTrue(result.Data.Mfa.Count > 1);
            Assert.IsTrue(result.Data.MfaType == MfaType.List);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task AddUser_Returns_Mfa_Device()
        {
            var result = await _userClient.AddUser("info", "device", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.Mfa);
            Assert.IsTrue(result.Data.MfaType == MfaType.Device);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task AddUser_Returns_UnknownInstitution()
        {
            var result = await _userClient.AddUser("info", "unknown", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
            Assert.IsNull(result.Data);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 1300);
        }
    }
}
