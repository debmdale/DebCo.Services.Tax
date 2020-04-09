using System.Collections.Generic;

namespace DebCo.Services.Tax.Providers.Abstractions
{
    public class Quote
    {
        public string QuoteId { get; set; }
        public string QuoteDate { get; set; }
        public string Provider { get; set; }
        public string ExemptionType { get; set; }
        public string ShipFromCountry { get; set; }
        public string ShipFromPostalCode { get; set; }
        public string ShipFromState { get; set; }
        public string ShipFromCity { get; set; }
        public string ShipFromStreet { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToStreet { get; set; }
        public decimal Amount { get; set; }
        public decimal Shipping { get; set; }
        public string CustomerId { get; set; }
        public List<QuoteLineItem> LineItems { get; set; }
    }
}
