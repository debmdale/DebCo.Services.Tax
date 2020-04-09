using System;
using System.Threading.Tasks;
using AutoMapper;
using DebCo.Services.Tax.Providers.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using TaxRatesRequest = DebCo.Services.Tax.Abstractions.TaxRatesRequest;
using TaxRatesResponse = DebCo.Services.Tax.Abstractions.TaxRatesResponse;

namespace DebCo.Services.Tax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxRatesController : ControllerBase
    {
        private readonly ILogger<TaxRatesController> _logger;
        private readonly IMapper _mapper;
        private readonly ITaxServiceProvider _client;

        public TaxRatesController(ILogger<TaxRatesController> logger, IMapper mapper, ITaxServiceProvider client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _client = client ?? throw new ArgumentNullException(nameof(client));

#if (DEBUG) 
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
#endif
        }

        // GET: api/TaxRates/12345
        [HttpGet("{id}")]
        [SwaggerOperation("Gets sales tax rates for a specified postal code", OperationId = "GetRates")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Get rates for postal code", Type = typeof(TaxRatesResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Get rates for postal code unsuccessful")]
        public async Task<ActionResult<TaxRatesResponse>> Get(string id)
        {
            _logger.LogInformation("Get Request for Id {@PostalCode}", id);

            try
            {
                var clientResult = await _client.GetRatesAsync(new Providers.Abstractions.TaxRatesRequest() { PostalCode = id }).ConfigureAwait(false);

                if (clientResult?.Rates == null)
                {
                    _logger.LogWarning("Get rates request for postal code {@PostalCode} unsuccessful: not found", id);
                    return NotFound(id);
                }

                var result = _mapper.Map<TaxRatesResponse>(clientResult);
                return Ok(result);

                //TODO: add some additional handling for specific service access exceptions
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rates for postal code {@PostalCode}", id);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST: api/TaxRates
        [HttpPost]
        [SwaggerOperation("Gets sales tax rates for a specified postal code", OperationId = "GetRatesForAddress")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Get rates for address", Type = typeof(TaxRatesResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Get rates for address unsuccessful")]
        public async Task<ActionResult<TaxRatesResponse>> Post([FromBody] TaxRatesRequest request)
        {
            _logger.LogInformation("Get rates for address {@Address}", request);

            try
            {
                var clientResult = await _client.GetRatesAsync(_mapper.Map<Providers.Abstractions.TaxRatesRequest>(request)).ConfigureAwait(false);

                if (clientResult?.Rates == null)
                {
                    _logger.LogWarning("Get rates request for address {@Address} unsuccessful: not found", request);
                    return NotFound(request);
                }

                var result = _mapper.Map<TaxRatesResponse>(clientResult);
                return Ok(result);

                //TODO: add some additional handling for specific service access exceptions

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting rates for address {@Address}", request);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

    }
}
