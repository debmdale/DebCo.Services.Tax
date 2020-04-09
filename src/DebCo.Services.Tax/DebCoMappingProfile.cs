using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DebCo.Services.Tax.Abstractions;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using JetBrains.Annotations;

namespace DebCo.Services.Tax
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class DebCoMappingProfile : Profile
    {
        public DebCoMappingProfile()
        {
            CreateMap<RateAddress, TaxRatesRequest>()
                .ForMember(dest => dest.PostalCode, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RateResponseAttributes, TaxRates>()
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Zip))
                .ReverseMap();
            
            CreateMap<RateResponse, TaxRatesResponse>()
                .ForMember(dest => dest.Rates, opt => opt.MapFrom(src => src.Rate))
                .ReverseMap();

            CreateMap<QuoteTaxLineItem, TaxBreakdownLineItem>().ReverseMap();

            CreateMap<QuoteShippingTaxDetail, TaxBreakdownShipping>().ReverseMap();

            CreateMap<QuoteTaxDetail, TaxBreakdown>()
                .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.ShippingTaxDetail))
                .ReverseMap();

            CreateMap<QuoteTaxJurisdictions, TaxJurisdictions>().ReverseMap();
            
            CreateMap<QuoteTax, TaxResponseAttributes>()
                .ForMember(dest => dest.Breakdown, opt => opt.MapFrom(src => src.QuoteTaxDetail))
                .ForMember(dest => dest.Jurisdictions, opt => opt.MapFrom(src => src.QuoteTaxJurisdictions))
                .ReverseMap();

            CreateMap<TaxQuoteResponse, TaxResponse>()
                .ForMember(dest => dest.Tax, opt => opt.MapFrom(src => src.QuoteTax))
                .ReverseMap();

            CreateMap<QuoteLineItem, LineItem>()
                .ForMember(dest => dest.SalesTax, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Quote, Order>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.QuoteId))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.QuoteDate))
                .ForMember(dest => dest.FromCountry, opt => opt.MapFrom(src => src.ShipFromCountry))
                .ForMember(dest => dest.FromZip, opt => opt.MapFrom(src => src.ShipFromPostalCode))
                .ForMember(dest => dest.FromCity, opt => opt.MapFrom(src => src.ShipFromCity))
                .ForMember(dest => dest.FromState, opt => opt.MapFrom(src => src.ShipFromState))
                .ForMember(dest => dest.FromStreet, opt => opt.MapFrom(src => src.ShipFromStreet))
                .ForMember(dest => dest.ToZip, opt => opt.MapFrom(src => src.ShipToPostalCode))
                .ForMember(dest => dest.ToCountry, opt => opt.MapFrom(src => src.ShipToCountry))
                .ForMember(dest => dest.ToCity, opt => opt.MapFrom(src => src.ShipToCity))
                .ForMember(dest => dest.ToState, opt => opt.MapFrom(src => src.ShipToState))
                .ForMember(dest => dest.ToStreet, opt => opt.MapFrom(src => src.ShipToStreet))
                .ForMember(dest => dest.SalesTax, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
