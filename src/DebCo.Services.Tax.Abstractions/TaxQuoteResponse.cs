using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class TaxQuoteResponse
    {
        [JsonProperty("quoteTax")]
        public QuoteTax QuoteTax { get; set; }
    }
}
