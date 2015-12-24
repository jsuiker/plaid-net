using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Plaid.Contracts;
using System.Threading.Tasks;
using Plaid.Tests.Fakes;

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

        [TestMethod]
        public void Ctor_Throws_MissingClientId()
        {
            try
            {
                var client = new PlaidUserClient(null, "test", "test");
                Assert.Fail("Expected exception.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Ctor_Throws_MissingSecret()
        {
            try
            {
                var client = new PlaidUserClient("test", null, "test");
                Assert.Fail("Expected exception.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Ctor_Throws_MissingEnv()
        {
            try
            {
                var client = new PlaidUserClient("test", "test", null);
                Assert.Fail("Expected exception.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
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

        [TestMethod]
        public async Task AddUser_Throws_MissingCredentials()
        {
            try
            {
                await _userClient.AddUser("test", "test", null, null);
                Assert.Fail("Expected exception.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
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

        [TestMethod]
        public async Task GetUser_Returns_OK()
        {
            var result = await _userClient.GetUser("info", "test", null);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Accounts.Count > 0);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetUser_Returns_Unauthorized()
        {
            var result = await _userClient.GetUser("info", "unknown", null);

            Assert.IsTrue(result.StatusCode == HttpStatusCode.Unauthorized);
            Assert.IsNull(result.Data);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 1105);
        }

        [TestMethod]
        public async Task DeleteUser_Returns_OK()
        {
            var result = await _userClient.DeleteUser("info", "test");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Message);
        }
    }
}
