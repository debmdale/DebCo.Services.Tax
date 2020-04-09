using Xunit;

namespace DebCo.Services.Tax.Providers.Tests.Fixtures
{
    [CollectionDefinition(Name)]
    public class TaxJarCollection : ICollectionFixture<TaxJarFixture>
    {
        public const string Name = "TaxJarProvider";
    }
}
