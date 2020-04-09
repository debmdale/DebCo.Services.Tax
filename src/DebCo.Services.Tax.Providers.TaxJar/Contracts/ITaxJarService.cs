using System.Threading.Tasks;

namespace DebCo.Services.Tax.Providers.TaxJar.Contracts
{
    public interface ITaxJarService
    {
        Task<TaxResponse> GetOrderTaxAsync(Order order);
        Task<RateResponse> GetRatesAsync(string zip, RateAddress address = null);
    }
}