using DebCo.Services.Tax.Options;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;

namespace DebCo.Services.Tax.Tests.Fixtures
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private IServiceScope _serviceScope;
        private readonly Mock<ITaxJarService> _taxJarServiceMock;
        public CustomWebApplicationFactory()
        {
            _taxJarServiceMock = new Mock<ITaxJarService>(MockBehavior.Loose);
        }


        // will automatically reset when accessed via Property
        public Mock<ITaxJarService> TaxJarServiceMock
        {
            get
            {
                _taxJarServiceMock.Reset();
                return _taxJarServiceMock;
            }
        }

        public ApplicationOptions ApplicationOptions { get; private set; }

        /// <inheritdoc />
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder
                .ConfigureServices(
                    services =>
                    {
                        //services.AddAutoMapper(typeof(DebCoMappingProfile));
                        services.AddSingleton(TaxJarServiceMock.Object);
                    });

            var testHost = base.CreateHost(builder);

            _serviceScope = testHost.Services.CreateScope();
            var serviceProvider = _serviceScope.ServiceProvider;
            ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;

            return testHost;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceScope?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
