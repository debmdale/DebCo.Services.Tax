using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class RateResponse
    {
        [JsonProperty("rate")]
        public RateResponseAttributes Rate { get; set; }
    }
}
