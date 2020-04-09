using System;
using System.Threading.Tasks;
using AutoMapper;
using DebCo.Services.Tax.Abstractions;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace DebCo.Services.Tax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxQuoteController : ControllerBase
    {
        private readonly ILogger<TaxQuoteController> _logger;
        private readonly IMapper _mapper;
        private readonly ITaxJarService _client;

        public TaxQuoteController(ILogger<TaxQuoteController> logger, IMapper mapper, ITaxJarService client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _client = client ?? throw new ArgumentNullException(nameof(client));

#if (DEBUG) 
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
#endif
        }

        // POST: api/TaxRates
        [HttpPost]
        [SwaggerOperation("The tax quote is used to estimate taxes on a proposed order",  OperationId = "GetQuoteForOrder")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Get quote for order", Type = typeof(TaxQuoteResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Get quote for order unsuccessful")]
        public async Task<ActionResult<TaxQuoteResponse>> Post([FromBody] TaxQuoteRequest request)
        {
            _logger.LogInformation("Get quote for order {@Order}", request);

            try
            {
                var clientResult = await _client.GetOrderTaxAsync(_mapper.Map<Order>(request.Quote)).ConfigureAwait(false);

                if (clientResult?.Tax == null)
                {
                    _logger.LogWarning("Get quote for order {@Order} unsuccessful: not found", request);
                    return NotFound(request);
                }

                var result = _mapper.Map<TaxQuoteResponse>(clientResult);
                return Ok(result);

                //TODO: add some additional handling for specific service access exceptions

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting quote for order {@Order}", request);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}