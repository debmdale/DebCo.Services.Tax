﻿using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class TaxJurisdictions
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}
