namespace Plaid.Tests.Fakes
{
    public class FakeSettings
    {
        public string Username { get; } = "plaid_test";

        public string CorrectPassword { get; } = "plaid_good";

        public string LockedPassword { get; } = "plaid_locked";

        public string Pin { get; } = "1234";
    }
}
