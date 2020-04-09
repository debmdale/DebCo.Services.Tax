using JetBrains.Annotations;

namespace DebCo.Services.Tax.Options
{
    [PublicAPI]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ApplicationOptions
    {
        public TaxJarOptions TaxJar { get; set; }
    }
}
