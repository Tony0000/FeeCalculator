using System.Threading.Tasks;
using Api.Common;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class InterestRateController : ControllerBase
    {
        private readonly IInterestRateService _service;

        public InterestRateController(IInterestRateService service)
        {
            _service = service;
        }

        [HttpGet(ApiRoutes.InterestRateProvider.TaxaJuros)]
        public async Task<IActionResult> Get()
        {
            var intRate = await _service.GetInterestRate();
            
            return Ok(intRate);
        }
    }
}