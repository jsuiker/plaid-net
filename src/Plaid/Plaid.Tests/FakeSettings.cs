using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.Tests
{
    public class FakeSettings
    {
        public string Username { get; } = "plaid_test";

        public string CorrectPassword { get; } = "plaid_good";

        public string LockedPassword { get; } = "plaid_locked";

        public string Pin { get; } = "1234";
    }
}
