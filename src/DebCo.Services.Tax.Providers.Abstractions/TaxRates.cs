

namespace DebCo.Services.Tax.Providers.Abstractions
{
    public class TaxRates
    {
        public string PostalCode { get; set; }
        public string State { get; set; }
        public decimal StateRate { get; set; }
        public string County { get; set; }
        public decimal CountyRate { get; set; }
        public string City { get; set; }
        public decimal CityRate { get; set; }
        public decimal CombinedDistrictRate { get; set; }
        public decimal CombinedRate { get; set; }
        public bool FreightTaxable { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public decimal CountryRate { get; set; }
        public decimal StandardRate { get; set; }
        public decimal ReducedRate { get; set; }
        public decimal SuperReducedRate { get; set; }
        public decimal ParkingRate { get; set; }
        public decimal DistanceSaleThreshold { get; set; }
    }
}
