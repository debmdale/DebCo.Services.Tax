using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DebCo.Services.Tax.Providers.TaxJar
{
    public class TaxJarService : ITaxJarService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<TaxJarService> _logger;

        public TaxJarService(IHttpClientFactory clientFactory, ILogger<TaxJarService> logger)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TaxResponse> GetOrderTaxAsync(Order order)
        {
            using (var client = GetClient())
            using (var response = await client.PostAsJsonAsync("taxes", order).ConfigureAwait(false))
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TaxResponse>(result);
                }

                await HandleResponseErrorAsync(response).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<RateResponse> GetRatesAsync(string zip, RateAddress address = null)
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response;
                if (address != null)
                {
                    using (var request = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{client.BaseAddress}/rates/{zip}"),
                        Content = new StringContent(JsonConvert.SerializeObject(address))
                    })
                    {
                        response = await client.SendAsync(request).ConfigureAwait(false);
                    }
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
