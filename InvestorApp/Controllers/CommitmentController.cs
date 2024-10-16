using Investor.Application.Services;
using Investor.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InvestorApp.Controllers
{
    [ApiController]
    [Route("api/commitments")]
    public class CommitmentController : Controller
    {
        private ICommitmentService _service;

        public CommitmentController(ICommitmentService service)
        {
            _service=service;
            
        }


        [HttpGet("{investorName}")]
        [ProducesResponseType(typeof(CommitmentsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCommitmentDetails([FromRoute] string investorName)
        {
            if (string.IsNullOrEmpty(investorName))
                return BadRequest("Investor Name cannot be empty");
            var data = _service.GetCommitments(investorName);
            return Ok(data);
        }
    }
}
