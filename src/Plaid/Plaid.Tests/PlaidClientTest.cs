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

        [TestMethod]
        public async Task AddUser_WithOptions_Returns_OK()
        {
            var result = await _client.AddUser("info", "citi", new Credentials { Username = "plaid_test", Password = "plaid_good" }, new Options());

            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public async Task AddUser_Throws_MissingCredentials()
        {
            await _client.AddUser("test", "test", null, null);
        }

        #region Institution Tests
        [TestMethod]
        public async Task GetInstitutions_Success()
        {
            var result = await _client.GetInstitutions();

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetInstitution_Id_Success()
        {
            var result = await _client.GetInstitution("5301a93ac140de84910000e0");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetInstitution_Id_NotFound()
        {
            var result = await _client.GetInstitution("unknown");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
            Assert.IsNull(result.Data);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 1301);
        }
        #endregion

        #region Category Tests
        [TestMethod]
        public async Task GetCategories_Success()
        {
            var result = await _client.GetCategories();

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetCategory_Id_Success()
        {
            var result = await _client.GetCategory("10000000");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetCategory_Id_NotFound()
        {
            var result = await _client.GetCategory("unknown");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
            Assert.IsNull(result.Data);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 1501);
        }
        #endregion
    }
}
