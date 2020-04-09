using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using DebCo.Services.Tax.Controllers;
using DebCo.Services.Tax.Providers.Abstractions;
using DebCo.Services.Tax.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace DebCo.Services.Tax.Tests
{
    public class TaxRatesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly Mock<ITaxServiceProvider> _taxServiceMock;
        private readonly HttpClient _client;

        public TaxRatesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _taxServiceMock = factory.TaxServiceMock;
        }

        [Fact]
        [Trait("Level","L0")]
        public void TaxRatesController_Constructor_ThrowsOnNullArguments()
        {
            var logger = NullLogger<TaxRatesController>.Instance;
            var mapper = new Mock<IMapper>().Object;
            var taxJar = new Mock<ITaxServiceProvider>().Object;

            Assert.Throws<ArgumentNullException>(() => new TaxRatesController(null, mapper, taxJar));
            Assert.Throws<ArgumentNullException>(() => new TaxRatesController(logger, null, taxJar));
            Assert.Throws<ArgumentNullException>(() => new TaxRatesController(logger, mapper, null));
        }

        [Fact]
        [Trait("Level","L0")]
        public async Task GetRatesById_Returns200_WhenFound()
        {

            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var rateResponse = fixture.Create<Providers.Abstractions.TaxRatesResponse>();
            
            _taxServiceMock.Setup(x => x.GetRatesAsync(It.IsAny<Providers.Abstractions.TaxRatesRequest>()))
                .ReturnsAsync(rateResponse);

            var response = await _client.GetAsync("api/taxrates/12345");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "Because a 200 response is expected");

        }

        [Fact]
        [Trait("Level","L0")]
        public async Task GetRatesById_Returns404_WhenNotFound()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var rateResponse = fixture.Create<Providers.Abstractions.TaxRatesResponse>();
            rateResponse.Rates = null;
            
            _taxServiceMock.Setup(x => x.GetRatesAsync(It.IsAny<Providers.Abstractions.TaxRatesRequest>()))
                .ReturnsAsync(rateResponse);

            var response = await _client.GetAsync("api/taxrates/12345");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Because a 404 response is expected");

        }
    }
}
