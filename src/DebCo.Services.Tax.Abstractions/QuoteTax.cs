using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class QuoteTax
    {
        [JsonProperty("orderTotalAmount")]
        public decimal OrderTotalAmount { get; set; }

        [JsonProperty("shipping")]
        public decimal Shipping { get; set; }

        [JsonProperty("taxableAmount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty("amountToCollect")]
        public decimal AmountToCollect { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("hasNexus")]
        public bool HasNexus { get; set; }

        [JsonProperty("freightTaxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("taxSource")]
        public string TaxSource { get; set; }

        [JsonProperty("exemptionType")]
        public string ExemptionType { get; set; }

        [JsonProperty("quoteJurisdictions")]
        public QuoteTaxJurisdictions QuoteTaxJurisdictions { get; set; }

        [JsonProperty("quoteTaxDetail")]
        public QuoteTaxDetail QuoteTaxDetail { get; set; }
        
    }
}
