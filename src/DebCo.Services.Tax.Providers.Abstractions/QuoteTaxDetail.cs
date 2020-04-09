using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DebCo.Services.Tax.Providers.Abstractions
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class QuoteTaxDetail
    {
        public decimal StateTaxRate { get; set; }
        public decimal StateTaxCollectable { get; set; }
        public decimal CountyTaxCollectable { get; set; }
        public decimal CityTaxCollectable { get; set; }
        public decimal SpecialDistrictTaxableAmount { get; set; }
        public decimal SpecialDistrictTaxRate { get; set; }
        public decimal SpecialDistrictTaxCollectable { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxCollectable { get; set; }
        public decimal CombinedTaxRate { get; set; }
        public decimal StateTaxableAmount { get; set; }
        public decimal CountyTaxableAmount { get; set; }
        public decimal CountyTaxRate { get; set; }
        public decimal CityTaxableAmount { get; set; }
        public decimal CityTaxRate { get; set; }
        public decimal CountryTaxableAmount { get; set; }
        public decimal CountryTaxRate { get; set; }
        public decimal CountryTaxCollectable { get; set; }
        public decimal GSTTaxableAmount { get; set; }
        public decimal GSTTaxRate { get; set; }
        public decimal GST { get; set; }
        public decimal PSTTaxableAmount { get; set; }
        public decimal PSTTaxRate { get; set; }
        public decimal PST { get; set; }
        public decimal QSTTaxableAmount { get; set; }
        public decimal QSTTaxRate { get; set; }
        public decimal QST { get; set; }
        public QuoteShippingTaxDetail ShippingTaxDetail { get; set; }
        public List<QuoteTaxLineItem> LineItems { get; set; }
    }
}
