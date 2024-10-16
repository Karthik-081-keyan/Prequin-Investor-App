using Investor.Application.Services;
using Investor.Domain.DTO;
using Investor.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace InvestorApp.Controllers
{
    [ApiController]
    [Route("api/investors")]
    public class InvestorController : Controller
    {
        public IInvestorService _service { get; }

        private ILogger<InvestorController> _logger;

        public InvestorController(IInvestorService service, ILogger<InvestorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<InvestorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllInvestors()
        {
            _logger.LogInformation("Executes GetAllInvestors");
            var records = _service.GetInvestors();
            if (records == null || !records.Any())
                return NotFound();
            return Ok(records);
        }


    }
}
