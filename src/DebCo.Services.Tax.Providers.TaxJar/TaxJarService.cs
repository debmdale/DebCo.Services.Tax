using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using DebCo.Services.Tax.Providers.Abstractions;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar
{
    public class TaxJarService : ITaxService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<TaxJarService> _logger;
        private readonly IMapper _mapper;

        public TaxJarService(IHttpClientFactory clientFactory, ILogger<TaxJarService> logger, IMapper mapper)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        
        public async Task<TaxQuoteResponse> GetOrderTaxAsync(Quote order)
        {
            var result = await GetOrderTaxAsync(_mapper.Map<Order>(order)).ConfigureAwait(false);
            return _mapper.Map<TaxQuoteResponse>(result);
        }

        public async Task<TaxRatesResponse> GetRatesAsync(TaxRatesRequest request)
        {
            RateAddress address = null;
            if (!string.IsNullOrEmpty(request.City) || !string.IsNullOrEmpty(request.Country) ||
                !string.IsNullOrEmpty(request.State) || !string.IsNullOrEmpty(request.Street))
            {
                address = _mapper.Map<RateAddress>(request);
            }

            var result = await GetRatesAsync(request.PostalCode, address).ConfigureAwait(false);
            return _mapper.Map<TaxRatesResponse>(result);
        }

        public async Task<TaxResponse> GetOrderTaxAsync(Order order)
        {
            using var client = GetClient();
            using var response = await client.PostAsJsonAsync("taxes", order).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TaxResponse>(result);
            }

            await HandleResponseErrorAsync(response).ConfigureAwait(false);
            return null;
        }

        public async Task<RateResponse> GetRatesAsync(string zip, RateAddress address = null)
        {
            using var client = GetClient();
            HttpResponseMessage response;
            if (address != null)
            {
                var queryParams = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(address.Country)) queryParams.Add("country", address.Country);
                if (!string.IsNullOrWhiteSpace(address.State)) queryParams.Add("state", address.State);
                if (!string.IsNullOrWhiteSpace(address.City)) queryParams.Add("city", address.City);
                if (!string.IsNullOrWhiteSpace(address.Street)) queryParams.Add("street", address.Street);
                var url = QueryHelpers.AddQueryString($"rates/{zip}", queryParams);
                response = await client.GetAsync(url).ConfigureAwait(false);
            }
            else
            {
                response = await client.GetAsync($"rates/{zip}").ConfigureAwait(false);
            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<RateResponse>(result);
            }

            await HandleResponseErrorAsync(response).ConfigureAwait(false);
            return null;
        }

        private async Task HandleResponseErrorAsync(HttpResponseMessage response)
        {
            _logger.LogInformation("Response error encountered {@ResponseError}", response);

            var errorResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            TaxjarError errMessage = new TaxjarError();
            try
            {
                errMessage = JsonConvert.DeserializeObject<TaxjarError>(errorResult);
            }
            // ReSharper disable once EmptyGeneralCatchClause - this is an intentional eating of any deserializing the response since it will get logged later
            catch { }


            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                {
                    throw new UnauthorizedAccessException(errMessage.Error);
                }
                case HttpStatusCode.Forbidden:
                {
                    throw new UnauthorizedAccessException(errMessage.Error);
                }
                case HttpStatusCode.NotFound:
                {
                    return;
                }
                default:
                {
                    throw new Exception($"{errMessage.StatusCode} : {errMessage.Error}, {errMessage.Detail}");
                }
            }
        }

        protected HttpClient GetClient()
        {
            return _clientFactory.CreateClient("TaxJar");
        }

    }
}
