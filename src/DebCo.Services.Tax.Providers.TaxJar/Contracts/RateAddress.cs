using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class RateAddress
    {
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
