using System.Threading.Tasks;

namespace DebCo.Services.Tax.Providers.Abstractions
{
    public interface ITaxService
    {
        Task<TaxQuoteResponse> GetOrderTaxAsync(Quote order);
        Task<TaxRatesResponse> GetRatesAsync(TaxRatesRequest request);
    }
}