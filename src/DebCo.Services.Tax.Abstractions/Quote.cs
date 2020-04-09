using System.Collections.Generic;
using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class Quote
    {
        [JsonProperty("quoteId")]
        public string QuoteId { get; set; }

        [JsonProperty("quoteDate")]
        public string QuoteDate { get; set; }

        [JsonProperty("provider", NullValueHandling = NullValueHandling.Ignore)]
        public string Provider { get; set; }

        [JsonProperty("exemptionType", NullValueHandling = NullValueHandling.Ignore)]
        public string ExemptionType { get; set; }

        [JsonProperty("shipFromCountry")]
        public string ShipFromCountry { get; set; }

        [JsonProperty("shipFromPostalCode")]
        public string ShipFromPostalCode { get; set; }

        [JsonProperty("shipFromState")]
        public string ShipFromState { get; set; }

        [JsonProperty("shipFromCity")]
        public string ShipFromCity { get; set; }

        [JsonProperty("shipFromStreet")]
        public string ShipFromStreet { get; set; }

        [JsonProperty("shipToCountry")]
        public string ShipToCountry { get; set; }

        [JsonProperty("shipToPostalCode")]
        public string ShipToPostalCode { get; set; }

        [JsonProperty("shipToState")]
        public string ShipToState { get; set; }

        [JsonProperty("shipToCity")]
        public string ShipToCity { get; set; }

        [JsonProperty("shipToStreet")]
        public string ShipToStreet { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty("shipping", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Shipping { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
        
        [JsonProperty("quoteLineItems")]
        public List<QuoteLineItem> LineItems { get; set; }
    }
}
