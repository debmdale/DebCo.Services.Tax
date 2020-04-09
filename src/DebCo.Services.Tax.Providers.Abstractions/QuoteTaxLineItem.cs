using System.Diagnostics.CodeAnalysis;

namespace DebCo.Services.Tax.Providers.Abstractions
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class QuoteTaxLineItem
    {
        public string Id { get; set; }
        public decimal StateSalesTaxRate { get; set; }
        public decimal StateAmount { get; set; }
        public decimal CountyAmount { get; set; }
        public decimal CityAmount { get; set; }
        public decimal SpecialDistrictTaxableAmount { get; set; }
        public decimal SpecialTaxRate { get; set; }
        public decimal SpecialDistrictAmount { get; set; }
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
    }
}
