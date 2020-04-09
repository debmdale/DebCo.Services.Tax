using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    // For real life scenarios, we would make sure naming was consistent...
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class QuoteTaxDetail
    {
        
        [JsonProperty("stateTaxRate")]
        public decimal StateTaxRate { get; set; }

        [JsonProperty("stateTaxCollectable")]
        public decimal StateTaxCollectable { get; set; }

        [JsonProperty("countyTaxCollectable")]
        public decimal CountyTaxCollectable { get; set; }

        [JsonProperty("cityTaxCollectable")]
        public decimal CityTaxCollectable { get; set; }

        [JsonProperty("specialDistrictTaxableAmount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("specialTaxRate")]
        public decimal SpecialDistrictTaxRate { get; set; }

        [JsonProperty("specialDistrictTaxCollectable")]
        public decimal SpecialDistrictTaxCollectable { get; set; }
        
        [JsonProperty("taxableAmount")]
        public decimal TaxableAmount { get; set; }

        [JsonProperty("taxCollectable")]
        public decimal TaxCollectable { get; set; }

        [JsonProperty("combinedTaxRate")]
        public decimal CombinedTaxRate { get; set; }

        [JsonProperty("stateTaxableAmount")]
        public decimal StateTaxableAmount { get; set; }

        [JsonProperty("countyTaxableAmount")]
        public decimal CountyTaxableAmount { get; set; }

        [JsonProperty("countyTaxRate")]
        public decimal CountyTaxRate { get; set; }

        [JsonProperty("cityTaxableAmount")]
        public decimal CityTaxableAmount { get; set; }

        [JsonProperty("cityTaxRate")]
        public decimal CityTaxRate { get; set; }

        // International
        [JsonProperty("countryTaxableAmount")]
        public decimal CountryTaxableAmount { get; set; }

        [JsonProperty("countryTaxRate")]
        public decimal CountryTaxRate { get; set; }

        [JsonProperty("countryTaxCollectable")]
        public decimal CountryTaxCollectable { get; set; }

        // Canada
        [JsonProperty("gstTaxableAmount")]
        public decimal GSTTaxableAmount { get; set; }

        [JsonProperty("gstTaxRate")]
        public decimal GSTTaxRate { get; set; }

        [JsonProperty("gst")]
        public decimal GST { get; set; }

        [JsonProperty("pstTaxableAmount")]
        public decimal PSTTaxableAmount { get; set; }

        [JsonProperty("pstTaxRate")]
        public decimal PSTTaxRate { get; set; }

        [JsonProperty("pst")]
        public decimal PST { get; set; }

        [JsonProperty("qstTaxableAmount")]
        public decimal QSTTaxableAmount { get; set; }

        [JsonProperty("qstTaxRate")]
        public decimal QSTTaxRate { get; set; }

        [JsonProperty("qst")]
        public decimal QST { get; set; }

        [JsonProperty("quoteShippingTaxDetail")]
        public QuoteShippingTaxDetail ShippingTaxDetail { get; set; }

        [JsonProperty("quoteTaxLineItem")]
        public List<QuoteTaxLineItem> LineItems { get; set; }
    }
}
