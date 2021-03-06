﻿
namespace DebCo.Services.Tax.Providers.Abstractions
{
    public class QuoteTax
    {
        public decimal OrderTotalAmount { get; set; }
        public decimal Shipping { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal AmountToCollect { get; set; }
        public decimal Rate { get; set; }
        public bool HasNexus { get; set; }
        public bool FreightTaxable { get; set; }
        public string TaxSource { get; set; }
        public string ExemptionType { get; set; }
        public QuoteTaxJurisdictions QuoteTaxJurisdictions { get; set; }
        public QuoteTaxDetail QuoteTaxDetail { get; set; }
        
    }
}
