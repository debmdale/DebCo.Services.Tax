using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    // For real life scenarios, we would make sure naming was consistent...
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class QuoteTaxLineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("stateSalesTaxRate")]
        public decimal StateSalesTaxRate { get; set; }

        [JsonProperty("stateAmount")]
        public decimal StateAmount { get; set; }

        [JsonProperty("countyAmount")]
        public decimal CountyAmount { get; set; }

        [JsonProperty("cityAmount")]
        public decimal CityAmount { get; set; }

        [JsonProperty("specialDistrictTaxableAmount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("specialTaxRate")]
        public decimal SpecialTaxRate { get; set; }

        [JsonProperty("specialDistrictAmount")]
        public decimal SpecialDistrictAmount { get; set; }
        
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
    }
}
