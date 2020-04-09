using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using DebCo.Services.Tax.Providers.TaxJar;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using DebCo.Services.Tax.Providers.Tests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace DebCo.Services.Tax.Providers.Tests
{
    [Collection(TaxJarCollection.Name)]
    public class TaxJarServiceTests
    {
        private readonly TaxJarFixture _fixture;
        private const string ClientName = "TaxJar";
        private readonly IMapper _mapper;

        public TaxJarServiceTests(TaxJarFixture fixture)
        {
            _fixture = fixture;
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(typeof(DebCoMappingProfile));
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        [Trait("Level","L0")]
        public void CanInstantiateNamedHttpClient()
        {
            _fixture.Factory.CreateClient(ClientName).BaseAddress.Should().Be("https://api.sandbox.taxjar.com/v2/", "The api named client has a base address configured");
            _fixture.Factory.CreateClient(ClientName).DefaultRequestHeaders.Authorization.Scheme.Should().Be("Bearer",
                "the api named client had a bearer authorization configured");
        }

        [Fact]
        [Trait("Level","L0")]
        public void TaxJarClientConstructorThrowsOnInvalidArguments()
        {
            var logger = NullLogger<TaxJarService>.Instance;
            var mapper = new Mock<IMapper>().Object;

            Assert.Throws<ArgumentNullException>(() => new TaxJarService(null, logger, mapper));
            Assert.Throws<ArgumentNullException>(() => new TaxJarService(_fixture.Factory, null, mapper));
            Assert.Throws<ArgumentNullException>(() => new TaxJarService(_fixture.Factory, logger, null));
        }

        [Fact]
        [Trait("Level","L0")]
        public async Task CanGetRatesForZip()
        {
            var logger = NullLogger<TaxJarService>.Instance;

            _fixture.Handler.SetupRequest(HttpMethod.Get, new Uri("https://api.sandbox.taxjar.com/v2/rates/90404"))
                .ReturnsResponse(HttpStatusCode.OK, responseMessage =>
                {
                    responseMessage.Content = new StringContent("{\r\n  \"rate\": {\r\n    \"zip\": \"90404\",\r\n    \"state\": \"CA\",\r\n    \"state_rate\": \"0.0625\",\r\n    \"county\": \"LOS ANGELES\",\r\n    \"county_rate\": \"0.01\",\r\n    \"city\": \"SANTA MONICA\",\r\n    \"city_rate\": \"0.0\",\r\n    \"combined_district_rate\": \"0.025\",\r\n    \"combined_rate\": \"0.0975\",\r\n    \"freight_taxable\": false\r\n  }\r\n}");
                });

            var client = new TaxJarService(_fixture.Factory, logger, _mapper);

            var response = await client.GetRatesAsync("90404");

            Assert.NotNull(response);
            Assert.NotNull(response.Rate);
            response.Rate.Zip.Should().Be("90404");
            response.Rate.State.Should().Be("CA");
            response.Rate.City.Should().Be("SANTA MONICA");
            response.Rate.County.Should().Be("LOS ANGELES");
            response.Rate.CityRate.Should().Be((decimal) 0.0);
            response.Rate.CountyRate.Should().Be((decimal) 0.01);
            response.Rate.StateRate.Should().Be((decimal) 0.0625);
            response.Rate.CombinedDistrictRate.Should().Be((decimal) 0.025);
            response.Rate.CombinedRate.Should().Be((decimal) 0.0975);
        }


        [Fact]
        [Trait("Level","L0")]
        public async Task CanGetRatesForZipWithAddressBody()
        {
            var logger = NullLogger<TaxJarService>.Instance;

            var address = new RateAddress {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Country = "US"
            };

            _fixture.Handler.SetupAnyRequest()
                .ReturnsResponse(HttpStatusCode.OK, responseMessage =>
                {
                    responseMessage.Content = new StringContent("{\r\n  \"rate\": {\r\n    \"zip\": \"05495-2086\",\r\n    \"country\": \"US\",\r\n    \"country_rate\": \"0.0\",\r\n    \"state\": \"VT\",\r\n    \"state_rate\": \"0.06\",\r\n    \"county\": \"CHITTENDEN\",\r\n    \"county_rate\": \"0.0\",\r\n    \"city\": \"WILLISTON\",\r\n    \"city_rate\": \"0.0\",\r\n    \"combined_district_rate\": \"0.01\",\r\n    \"combined_rate\": \"0.07\",\r\n    \"freight_taxable\": true\r\n  }\r\n}");
                });

            var client = new TaxJarService(_fixture.Factory, logger, _mapper);

            var response = await client.GetRatesAsync("05495-2086", address);

            Assert.NotNull(response);
            Assert.NotNull(response.Rate);
            response.Rate.Zip.Should().Be("05495-2086");
            response.Rate.Country.Should().Be(address.Country);
            response.Rate.State.Should().Be(address.State);
            response.Rate.City.Should().Be("WILLISTON");
            response.Rate.County.Should().Be("CHITTENDEN");
            response.Rate.CountryRate.Should().Be((decimal) 0.0);
            response.Rate.CityRate.Should().Be((decimal) 0.0);
            response.Rate.CountyRate.Should().Be((decimal) 0.0);
            response.Rate.StateRate.Should().Be((decimal) 0.06);
            response.Rate.CombinedDistrictRate.Should().Be((decimal) 0.01);
            response.Rate.CombinedRate.Should().Be((decimal) 0.07);
        }


        [Fact]
        [Trait("Level","L0")]
        public async Task CanGetTaxForOrder()
        {
            var logger = NullLogger<TaxJarService>.Instance;

            var order = new Order()
            {
                FromCountry = "US",
                FromZip = "92093",
                FromState = "CA",
                FromCity = "La Jolla",
                FromStreet = "9500 Gilman Drive",
                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                ToCity = "Los Angeles",
                ToStreet = "1335 E 103rd St",
                Amount = 15,
                Shipping = (decimal) 1.5,
                LineItems = new List<LineItem>()
                {
                    new LineItem()
                    {
                        Id = "1",
                        Quantity = 1,
                        ProductTaxCode = "20010",
                        UnitPrice = 15,
                        Discount = 0
                    }
                }
            };

            _fixture.Handler.SetupRequest(HttpMethod.Post, new Uri("https://api.sandbox.taxjar.com/v2/taxes"))
                .ReturnsResponse(HttpStatusCode.OK, responseMessage =>
                {
                    responseMessage.Content = new StringContent("{\r\n  \"tax\": {\r\n    \"order_total_amount\": 16.5,\r\n    \"shipping\": 1.5,\r\n    \"taxable_amount\": 15,\r\n    \"amount_to_collect\": 1.35,\r\n    \"rate\": 0.09,\r\n    \"has_nexus\": true,\r\n    \"freight_taxable\": false,\r\n    \"tax_source\": \"destination\",\r\n    \"jurisdictions\": {\r\n      \"country\": \"US\",\r\n      \"state\": \"CA\",\r\n      \"county\": \"LOS ANGELES\",\r\n      \"city\": \"LOS ANGELES\"\r\n    },\r\n    \"breakdown\": {\r\n      \"taxable_amount\": 15,\r\n      \"tax_collectable\": 1.35,\r\n      \"combined_tax_rate\": 0.09,\r\n      \"state_taxable_amount\": 15,\r\n      \"state_tax_rate\": 0.0625,\r\n      \"state_tax_collectable\": 0.94,\r\n      \"county_taxable_amount\": 15,\r\n      \"county_tax_rate\": 0.0025,\r\n      \"county_tax_collectable\": 0.04,\r\n      \"city_taxable_amount\": 0,\r\n      \"city_tax_rate\": 0,\r\n      \"city_tax_collectable\": 0,\r\n      \"special_district_taxable_amount\": 15,\r\n      \"special_tax_rate\": 0.025,\r\n      \"special_district_tax_collectable\": 0.38,\r\n      \"line_items\": [\r\n        {\r\n          \"id\": \"1\",\r\n          \"taxable_amount\": 15,\r\n          \"tax_collectable\": 1.35,\r\n          \"combined_tax_rate\": 0.09,\r\n          \"state_taxable_amount\": 15,\r\n          \"state_sales_tax_rate\": 0.0625,\r\n          \"state_amount\": 0.94,\r\n          \"county_taxable_amount\": 15,\r\n          \"county_tax_rate\": 0.0025,\r\n          \"county_amount\": 0.04,\r\n          \"city_taxable_amount\": 0,\r\n          \"city_tax_rate\": 0,\r\n          \"city_amount\": 0,\r\n          \"special_district_taxable_amount\": 15,\r\n          \"special_tax_rate\": 0.025,\r\n          \"special_district_amount\": 0.38\r\n        }\r\n      ]\r\n    }\r\n  }\r\n}");
                });

            var client = new TaxJarService(_fixture.Factory, logger, _mapper);

            var response = await client.GetOrderTaxAsync(order);

            Assert.NotNull(response);
            Assert.NotNull(response.Tax);
            Assert.NotNull(response.Tax.Jurisdictions);
            Assert.NotNull(response.Tax.Breakdown);
            Assert.NotNull(response.Tax.Breakdown.LineItems);

            response.Tax.OrderTotalAmount.Should().Be((decimal) 16.5);
            response.Tax.Shipping.Should().Be((decimal) 1.5);
            response.Tax.TaxableAmount.Should().Be(15);
            response.Tax.AmountToCollect.Should().Be((decimal) 1.35);
            response.Tax.Rate.Should().Be((decimal) 0.09);
            response.Tax.HasNexus.Should().Be(true);
            response.Tax.FreightTaxable.Should().Be(false);
            response.Tax.TaxSource.Should().Be("destination");

        }
    }
}
