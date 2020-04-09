using DebCo.Services.Tax.Options;
using DebCo.Services.Tax.Providers.Abstractions;
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
        private readonly Mock<ITaxServiceProvider> _taxServiceMock;
        public CustomWebApplicationFactory()
        {
            _taxServiceMock = new Mock<ITaxServiceProvider>(MockBehavior.Loose);
        }


        // will automatically reset when accessed via Property
        public Mock<ITaxServiceProvider> TaxServiceMock
        {
            get
            {
                _taxServiceMock.Reset();
                return _taxServiceMock;
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
                        services.AddSingleton(TaxServiceMock.Object);
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
