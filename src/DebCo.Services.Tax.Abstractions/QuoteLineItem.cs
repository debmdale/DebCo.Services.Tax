using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class QuoteLineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("productIdentifier")]
        public string ProductIdentifier { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("productTaxCode")]
        public string ProductTaxCode { get; set; }

        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
}
