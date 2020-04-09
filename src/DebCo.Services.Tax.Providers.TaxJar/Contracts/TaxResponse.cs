using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class TaxResponse
    {
        [JsonProperty("tax")]
        public TaxResponseAttributes Tax { get; set; }
    }
}
