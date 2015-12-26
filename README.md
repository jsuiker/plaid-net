# Plaid
###### .NET bindings for the Plaid API

This library provides convenient access to the [Plaid restful services](https://plaid.com/docs/). It's also available as [NuGet package](https://www.nuget.org/packages/Plaid/) for .NET and Universal Windows Platform. Please [drop me a note](https://github.com/tmarkovski/plaid-net/issues) if you need specific target frameworks.

#### Usage
_Public Client_
```csharp
  var client = new PlaidPublicClient(PlaidClient.EnvironmentDevelopment);

  var institutions = await client.GetInstitutions();
```


_User Client_
```csharp
  var userClient = new PlaidUserClient("test_id", "test_secret", PlaidClient.EnvironmentDevelopment);
  
  var response = await userClient.AddUser("connect", "chase",
    new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);

  switch (response.StatusCode)
  {
      case HttpStatusCode.OK:
          var userData = response.Data;

          // Account list -> userData.Accounts
          // Transactions -> userData.Transactions
          // User info    -> userData.Info

          break;
      case HttpStatusCode.Created:

          // Multi-factor authentication
          // Details about response in response.Mfa

          response = await userClient.StepUser("connect", response.Data.AccessToken, new[] { "tomato" }, null);
          break;
      default:
          // inspect response.Error for details and messages
          break;
  }
```
