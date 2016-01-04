# Plaid
###### .NET bindings for the Plaid API
[![Build status](https://ci.appveyor.com/api/projects/status/as0caj6uoqjsjl2p?svg=true)](https://ci.appveyor.com/project/TomislavMarkovski/plaid-net)

This library provides convenient access to the [Plaid restful services](https://plaid.com/docs/). It's also available as [NuGet package](https://www.nuget.org/packages/Plaid/) for .NET, Universal Windows Platform, Portable Class Library (Xamarin). Please [drop me a note](https://github.com/tmarkovski/plaid-net/issues) if you need specific target frameworks.

#### Usage
_Public Client_
```csharp
  var client = new PlaidPublicClient(PlaidClient.EnvironmentDevelopment);

  var institutions = await client.GetInstitutions();
```


_User Client_
```csharp
  var client = new PlaidUserClient("test_id", "test_secret", PlaidClient.EnvironmentDevelopment);
  var product = "connect";
            
  try
  {
      var response = await client.AddUser(product, "td", 
        new Credentials { Username = "plaid_test", Password = "plaid_good" }, null);
      
      // if response contains multi factor authentication, details will 
      // be wrapped in "Mfa" property
      if (response.Mfa != null)
          await client.StepUser(product, response.Mfa.AccessToken, new[] {"tomato"}, null);
      else
          // response.Accounts      -> Account information
          // response.Transactions  -> Transactions
          // response.Info          -> User information from this institution
  }
  catch (PlaidException e)
  {
      // Use this exception to capture Plaid API errors 
      // as specified in https://plaid.com/docs/#response-codes
      // Error details wrapped in e.Error
      // 
      // if using WCF, you can easily use WebFaultException to handle the error
      // throw new WebFaultException<Error>(e.Error, e.Error.StatusCode);
  }
  catch (Exception e)
  {
      // Something else happened here
  }
```
