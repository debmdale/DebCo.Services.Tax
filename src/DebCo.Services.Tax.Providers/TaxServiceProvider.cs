using DebCo.Services.Tax.Providers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebCo.Services.Tax.Providers
{
    public class TaxServiceProvider : ITaxServiceProvider
    {
        private readonly IEnumerable<ITaxService> _providers;

        public TaxServiceProvider(IEnumerable<ITaxService> providers)
        {
            _providers = providers ?? throw new ArgumentNullException(nameof(providers));
        }

        public async Task<TaxQuoteResponse> GetOrderTaxAsync(Quote order)
        {
            //TODO: add some decision making criteria here which would determine which provider picks up the work.
            if (_providers != null) return await _providers.First().GetOrderTaxAsync(order).ConfigureAwait(false);
            throw new NotImplementedException();
        }

        public async Task<TaxRatesResponse> GetRatesAsync(TaxRatesRequest request)
        {
            //TODO: add some decision making criteria here which would determine which provider picks up the work.
            if (_providers != null) return await _providers.First().GetRatesAsync(request).ConfigureAwait(false);
            throw new NotImplementedException();
        }
    }
}
