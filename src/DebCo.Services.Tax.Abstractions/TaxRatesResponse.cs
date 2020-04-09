using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class TaxRatesResponse
    {
        [JsonProperty("rates")]
        public TaxRates Rates { get; set; }
    }
    
}
