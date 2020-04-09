using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Moq;
using Moq.Contrib.HttpClient;

namespace DebCo.Services.Tax.Providers.Tests.Fixtures
{
    public class TaxJarFixture : IDisposable
    {
        private const string AccessToken = "1d0a3864f7ffba965da191f14b7b5b12";

        public TaxJarFixture()
        {
            _handler = new Mock<HttpMessageHandler>();
            Factory = SetupFactory(_handler);
        }

        private readonly Mock<HttpMessageHandler> _handler;

        public Mock<HttpMessageHandler> Handler => _handler;

        public IHttpClientFactory Factory { get; }


        private static IHttpClientFactory SetupFactory(Mock<HttpMessageHandler> handler)
        {

            var factory = handler.CreateClientFactory();

            Mock.Get(factory).Setup(x => x.CreateClient("TaxJar"))
                .Returns(() =>
                {
                    var client = handler.CreateClient();
                    client.BaseAddress = new Uri("https://api.sandbox.taxjar.com/v2/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                    return client;
                });
            return factory;
        }


        public void Dispose()
        {
            
        }
    }
}
