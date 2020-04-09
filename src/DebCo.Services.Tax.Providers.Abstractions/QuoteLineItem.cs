
namespace DebCo.Services.Tax.Providers.Abstractions
{
    public class QuoteLineItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string ProductIdentifier { get; set; }
        public string Description { get; set; }
        public string ProductTaxCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
