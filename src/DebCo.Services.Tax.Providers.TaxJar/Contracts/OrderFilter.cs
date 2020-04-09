using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public class OrderFilter
    {
        [JsonProperty("transaction_date")]
        public string TransactionDate { get; set; }

        [JsonProperty("from_transaction_date")]
        public string FromTransactionDate { get; set; }

        [JsonProperty("to_transaction_date")]
        public string ToTransactionDate { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }
    }
}