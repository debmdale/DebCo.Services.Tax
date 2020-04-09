using JetBrains.Annotations;

namespace DebCo.Services.Tax.Options
{
    [PublicAPI]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TaxJarOptions
    {
        public string ServiceUrl { get; set; }
        public string ServiceVersion { get; set; }
        public string Token { get; set; }
    }
}
