using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Plaid.Exceptions;
#if WINDOWS_UWP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Plaid.Tests.Fakes;

namespace Plaid.Tests
{
    [TestClass]
    public class PlaidPublicClientTests
    {
        private readonly IPlaidPublicClient _publicClient;

        public PlaidPublicClientTests()
        {
            _publicClient = new FakePlaidPublicClient();
        }

        #region Institution Tests
        [TestMethod]
        public async Task GetInstitutions_Success()
        {
            var result = await _publicClient.GetInstitutions();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetInstitution_Id_Success()
        {
            var result = await _publicClient.GetInstitution("5301a93ac140de84910000e0");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetInstitution_Id_NotFound()
        {
            try
            {
                var result = await _publicClient.GetInstitution("unknown");

                Assert.Fail();
            }
            catch (PlaidException e)
            {
                Assert.IsNotNull(e.Error);
                Assert.IsTrue(e.Error.Code == 1301);
                Assert.IsTrue(e.Error.StatusCode == HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region Category Tests
        [TestMethod]
        public async Task GetCategories_Success()
        {
            var result = await _publicClient.GetCategories();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetCategory_Id_Success()
        {
            var result = await _publicClient.GetCategory("10000000");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetCategory_Id_NotFound()
        {
            try
            {
                var result = await _publicClient.GetCategory("unknown");

                Assert.Fail();
            }
            catch (PlaidException e)
            {
                Assert.IsNotNull(e.Error);
                Assert.IsTrue(e.Error.Code == 1501);
                Assert.IsTrue(e.Error.StatusCode == HttpStatusCode.NotFound);
            }
        }
        #endregion
    }
}
