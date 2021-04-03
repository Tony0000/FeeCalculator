using System.Threading.Tasks;
using Api.Common;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class FeeCalculatorController : ControllerBase
    {
        private readonly IFeeCalculatorService _service;
        
        public FeeCalculatorController(IFeeCalculatorService service)
        {
            _service = service;
        }

        
        /// <summary>
        /// Calculates fee with interests
        /// </summary>
        /// <response code="200">Returns the fee</response>
        /// <response code="400">If the query values are null or invalid</response>  
        [HttpGet(ApiRoutes.FeeCalculator.CalcFee)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CalculateFee([FromQuery]FeeQuery query)
        {
            var fee = await _service.CalculateFees(query);
            return Ok(fee.ToString("F2"));
        }
        
        /// <summary>
        /// Returns github repository url
        /// </summary>
        /// <response code="200">Returns the github url</response>
        [HttpGet(ApiRoutes.FeeCalculator.ShowMeTheCode)]
        public async Task<IActionResult> GetUrl()
        {
            var url = await _service.GetGithubUrl();
            return Ok(url);
        }
    }
}