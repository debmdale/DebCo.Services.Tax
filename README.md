# DebCo.Services.Tax

This solution contains the following projects:

- DebCo.Services.Tax (API Site - .NET CORE 3.1)
- DebCo.Services.Tax.Abstractions (API-related Abstractions - .NET STANDARD 2.1)
- DebCo.Services.Tax.Providers.TaxJar (TaxJar Tax Service Provider - .NET STANDARD 2.1)
- DebCo.Services.Tax.Tests (Tests for the API Site - .NET CORE 3.1)
- DebCo.Services.Tax.Providers.Tests (Tests for the Tax Service Providers - .NET CORE 3.1)

This is a simple solution showing how to create your own REST API service wrapper around an external third party service provider. 
The example for this solution is a Tax service, and I've integrated two methods available from the TaxJar service provider here.
This solution could easily be expanded to integrate multiple tax services, allowing the requester to distinguish which provider
to request tax data from, without having to worry about integrating with that provider directly.  Or perhaps you'd like to write
your own tax provider and alternatively offer that as well.  Simply add a DebCo.Services.Tax.Providers library (.NET STANDARD 2.1)
with the necessary independently defined interfaces that would determine how the API would internally source data and return it
back to the requester.

In this solution you will also find a documentation folder, which contains two different live UML code maps for ease of reference.
Additionally there are two different high level UML diagrams, showing current and future state as described above.