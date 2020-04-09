using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class TaxjarError
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("status")]
        public string StatusCode { get; set; }
    }
}
