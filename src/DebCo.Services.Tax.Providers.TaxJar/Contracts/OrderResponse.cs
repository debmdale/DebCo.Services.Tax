using System.Collections.Generic;
using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class OrdersResponse
    {
        [JsonProperty("orders")]
        public List<string> Orders { get; set; }
    }

    public class OrderResponse
    {
        [JsonProperty("order")]
        public OrderResponseAttributes Order { get; set; }
    }

}
