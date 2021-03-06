﻿using System;
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
using Newtonsoft.Json;
using Xunit;

namespace DebCo.Services.Tax.Tests
{
    public class TaxQuoteControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly Mock<ITaxServiceProvider> _taxServiceMock;
        private readonly HttpClient _client;

        public TaxQuoteControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _taxServiceMock = factory.TaxServiceMock;
        }

        [Fact]
        [Trait("Level","L0")]
        public void TaxQuoteController_Constructor_ThrowsOnNullArguments()
        {
            var logger = NullLogger<TaxQuoteController>.Instance;
            var mapper = new Mock<IMapper>().Object;
            var taxJar = new Mock<ITaxServiceProvider>().Object;

            Assert.Throws<ArgumentNullException>(() => new TaxQuoteController(null, mapper, taxJar));
            Assert.Throws<ArgumentNullException>(() => new TaxQuoteController(logger, null, taxJar));
            Assert.Throws<ArgumentNullException>(() => new TaxQuoteController(logger, mapper, null));
        }

        
        [Fact]
        [Trait("Level","L0")]
        public async Task TaxQuotePost_Returns200_WhenSuccessful()
        {

            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var taxResponse = fixture.Create<Providers.Abstractions.TaxQuoteResponse>();
            var quoteRequest = fixture.Create<Providers.Abstractions.TaxQuoteRequest>();
            
            _taxServiceMock.Setup(x => x.GetOrderTaxAsync(It.IsAny<Quote>()))
                .ReturnsAsync(taxResponse);

            var response = await _client.PostAsJsonAsync("api/taxquote/", new StringContent(JsonConvert.SerializeObject(quoteRequest)));
            response.StatusCode.Should().Be(HttpStatusCode.OK, "Because a 200 response is expected");

        }
        
        [Fact]
        [Trait("Level","L0")]
        public async Task TaxQuotePost_Returns404_WhenNotFound()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var taxResponse = fixture.Create<Providers.Abstractions.TaxQuoteResponse>();
            taxResponse.QuoteTax = null;
            var quoteRequest = fixture.Create<Providers.Abstractions.TaxQuoteRequest>();
            
            _taxServiceMock.Setup(x => x.GetOrderTaxAsync(It.IsAny<Quote>()))
                .ReturnsAsync(taxResponse);

            var response = await _client.PostAsJsonAsync("api/taxquote/", new StringContent(JsonConvert.SerializeObject(quoteRequest)));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Because a 404 response is expected");

        }
    }
}
