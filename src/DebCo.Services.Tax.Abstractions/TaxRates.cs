using Newtonsoft.Json;

namespace DebCo.Services.Tax.Abstractions
{
    public class TaxRates
    {
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stateRate")]
        public decimal StateRate { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("countyRate")]
        public decimal CountyRate { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("cityRate")]
        public decimal CityRate { get; set; }

        [JsonProperty("combinedDistrictRate")]
        public decimal CombinedDistrictRate { get; set; }

        [JsonProperty("combinedRate")]
        public decimal CombinedRate { get; set; }

        [JsonProperty("freightTaxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("countryRate")]
        public decimal CountryRate { get; set; }

        [JsonProperty("standardRate")]
        public decimal StandardRate { get; set; }

        [JsonProperty("reducedRate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ReducedRate { get; set; }

        [JsonProperty("superReducedRate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SuperReducedRate { get; set; }

        [JsonProperty("parkingRate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ParkingRate { get; set; }

        [JsonProperty("distanceSaleThreshold", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DistanceSaleThreshold { get; set; }
    }
}
