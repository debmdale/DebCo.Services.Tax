using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using DebCo.Services.Tax.Controllers;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using DebCo.Services.Tax.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace DebCo.Services.Tax.Tests
{
    public class TaxRatesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly Mock<ITaxJarService> _taxJarServiceMock;
        private readonly HttpClient _client;

        public TaxRatesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _taxJarServiceMock = factory.TaxJarServiceMock;
        }

        [Fact]
        public void TaxRatesController_Constructor_ThrowsOnNullArguments()
        {
            var logger = NullLogger<TaxRatesController>.Instance;
            var mapper = new Mock<IMapper>().Object;
            var taxJar = new Mock<ITaxJarService>().Object;

            Assert.Throws<ArgumentNullException>(() => new TaxRatesController(null, mapper, taxJar));
            Assert.Throws<ArgumentNullException>(() => new TaxRatesController(logger, null, taxJar));
            Assert.Throws<ArgumentNullException>(() => new TaxRatesController(logger, mapper, null));
        }

        [Fact]
        public async Task GetRatesById_Returns200_WhenFound()
        {

            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var rateResponse = fixture.Create<RateResponse>();
            
            _taxJarServiceMock.Setup(x => x.GetRatesAsync(It.IsAny<string>(), null))
                .ReturnsAsync(rateResponse);

            var response = await _client.GetAsync("api/taxrates/12345");
            response.StatusCode.Should().Be(HttpStatusCode.OK, "Because a 200 response is expected");

        }

        [Fact]
        public async Task GetRatesById_Returns404_WhenNotFound()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var rateResponse = fixture.Create<RateResponse>();
            rateResponse.Rate = null;
            
            _taxJarServiceMock.Setup(x => x.GetRatesAsync(It.IsAny<string>(), null))
                .ReturnsAsync(rateResponse);

            var response = await _client.GetAsync("api/taxrates/12345");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Because a 404 response is expected");

        }
    }
}
