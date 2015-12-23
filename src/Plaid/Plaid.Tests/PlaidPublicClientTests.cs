using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetInstitution_Id_Success()
        {
            var result = await _publicClient.GetInstitution("5301a93ac140de84910000e0");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetInstitution_Id_NotFound()
        {
            var result = await _publicClient.GetInstitution("unknown");

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
            var result = await _publicClient.GetCategories();

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetCategory_Id_Success()
        {
            var result = await _publicClient.GetCategory("10000000");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public async Task GetCategory_Id_NotFound()
        {
            var result = await _publicClient.GetCategory("unknown");

            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
            Assert.IsNull(result.Data);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 1501);
        }
        #endregion
    }
}
