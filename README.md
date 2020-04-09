# DebCo.Services.Tax

This solution contains the following projects:

- DebCo.Services.Tax (API Site - .NET CORE 3.1)
- DebCo.Services.Tax.Abstractions (API-related Abstractions - .NET STANDARD 2.1)
- DebCo.Services.Tax.Providers (Tax Service Provider - .NET STANDARD 2.1)
- DebCo.Services.Tax.Providers.Abstractions (Tax Service Provider related Abstractions - .NET STANDARD 2.1)
- DebCo.Services.Tax.Providers.TaxJar (TaxJar Tax Service Provider - .NET STANDARD 2.1)
- DebCo.Services.Tax.Tests (Tests for the API Site - .NET CORE 3.1)
- DebCo.Services.Tax.Providers.Tests (Tests for the Tax Service Providers - .NET CORE 3.1)

This is a simple solution showing how to create your own REST API service wrapper around an external third party service provider. 
The example for this solution is a Tax service, and I've integrated two methods available from the TaxJar service provider here.
This solution could easily be expanded to integrate multiple tax services, allowing the requester to distinguish which provider
to request tax data from, without having to worry about integrating with that provider directly.  Or perhaps you'd like to write
your own tax provider and alternatively offer that as well.  Simply have the new provider implement the ITaxService interface 
found in the DebCo.Services.Tax.Providers.Abstractions library, and register it as a scoped service in the Startup of DebCo.Services.Tax.
The TaxServiceProvider is currently randomly selecting a provider to service the request, however a little refactoring could easily 
let some data about the customer making the request determine which provider should be picked to respond.

In this solution you will also find a documentation folder, which contains two different UML code maps for ease of reference.
Additionally there are two different high level UML diagrams, showing current and future state as described above.

Note: This solution makes use of AutoMapper.  Currently the mapping profile for AutoMapper is residing as a single profile in DebCo.Services.Tax. 
Ideally, this could be split into multiple profiles to enable removing the reference to the DebCo.Services.Tax.Providers.TaxJar project. 
It lives here for simplicity only, since this is just a sample project.