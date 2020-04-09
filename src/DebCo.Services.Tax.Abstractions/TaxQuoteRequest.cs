using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class TaxQuoteRequest
    {
        [JsonProperty("quote")]
        public Quote Quote { get; set; }
    }
}
