using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Plaid.Contracts;
using System.Threading.Tasks;
using Plaid.Exceptions;
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

            Assert.IsNotNull(result);
            Assert.IsNull(result.Mfa);
        }

        [TestMethod]
        public async Task AddUser_WithOptions_Returns_OK()
        {
            var result = await _userClient.AddUser("info", "citi", new Credentials { Username = "plaid_test", Password = "plaid_good" }, new Options());

            Assert.IsNotNull(result);
            Assert.IsNull(result.Mfa);
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Mfa);
            Assert.IsTrue(result.Mfa.Items.Count > 0);
            Assert.IsTrue(result.Mfa.Type == "questions");
        }

        [TestMethod]
        public async Task AddUser_Returns_Mfa_Selections()
        {
            var result = await _userClient.AddUser("info", "selections", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.IsNotNull(result.Mfa);
            Assert.IsTrue(result.Mfa.Items.Count > 0);
            Assert.IsTrue(result.Mfa.Items.First().Answers.Count > 0);
            Assert.IsTrue(result.Mfa.Type == "selections");
        }

        [TestMethod]
        public async Task AddUser_Returns_Mfa_List()
        {
            var result = await _userClient.AddUser("info", "list", new Credentials { Username = "plaid_test", Password = "plaid_good" }, new Options { List = true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Mfa);
            Assert.IsTrue(result.Mfa.Items.Count > 1);
            Assert.IsTrue(result.Mfa.Type == "list");
        }

        [TestMethod]
        public async Task AddUser_Returns_Mfa_Device()
        {
            var result = await _userClient.AddUser("info", "device", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Mfa);
            Assert.IsTrue(result.Mfa.Type == "device");
            Assert.IsTrue(result.Mfa.AccessToken != null);
        }

        [TestMethod]
        public async Task AddUser_Returns_UnknownInstitution()
        {
            try
            {
                await _userClient.AddUser("info", "unknown", new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);
                Assert.Fail();
            }
            catch (PlaidException e)
            {
                Assert.IsNotNull(e.Error);
                Assert.IsTrue(e.Error.Code == 1300);
                Assert.IsTrue(e.Error.StatusCode == HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public async Task GetUser_Returns_OK()
        {
            var result = await _userClient.GetUser("info", "test", null);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Accounts.Count > 0);
            Assert.IsNotNull(result.Info.Addresses[0]);
        }

        [TestMethod]
        public async Task GetUser_Restursn_OK_AlternateAddressFormat()
        {
            var result = await _userClient.GetUser("info", "test_1", null);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Accounts.Count > 0);
            Assert.IsNotNull(result.Info.Addresses[0]);
        }

        [TestMethod]
        public async Task GetUser_Returns_Unauthorized()
        {
            try
            {
                await _userClient.GetUser("info", "unknown", null);
                Assert.Fail();
            }
            catch (PlaidException e)
            {
                Assert.IsNotNull(e.Error);
                Assert.IsTrue(e.Error.StatusCode == HttpStatusCode.Unauthorized);
                Assert.IsTrue(e.Error.Code == 1105);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task DeleteUser_Returns_OK()
        {
            try
            {
                await _userClient.DeleteUser("info", "test");
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task UpgradeUser_Returns_OK()
        {
            var result = await _userClient.UpgradeUser("test_chase", "test", null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpgradeUser_ProductUnavailable()
        {
            try
            {
                await _userClient.UpgradeUser("test_td", "connect", null);
                Assert.Fail();
            }
            catch (PlaidException e)
            {
                Assert.IsNotNull(e.Error);
                Assert.IsTrue(e.Error.StatusCode == HttpStatusCode.NotFound);
                Assert.IsTrue(e.Error.Code == 1601);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetBalance_Returns_OK()
        {
            var result = await _userClient.GetBalance("test");
            Assert.IsNotNull(result);
        }
    }
}
