using Investor.Application.Services;
using Investor.Domain.DTO;
using InvestorApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InvestApp.Test.Controller
{
    public class InvestorControllerTest
    {
        private readonly InvestorController _controller;
        private Mock<IInvestorService> _service;
        private Mock<ILogger<InvestorController>> _logger;
        public InvestorControllerTest()
        {
            _service = new Mock<IInvestorService>();
            _logger = new Mock<ILogger<InvestorController>>();
            _controller = new InvestorController(_service.Object, _logger.Object);
        }

        [OneTimeTearDown]
        public void cleanUp()
        {
            _controller.Dispose();
            _controller.Dispose();
        }

        [Test]
        public void GetAllInvestors_Returns_InvestorData()
        {
            //arrange
            var data = Enumerable.Range(0, 10).Select(i => new InvestorDto { Id = i }).ToList();
            _service.Setup(i => i.GetInvestors()).Returns(data);

            //act
            var result = (OkObjectResult)_controller.GetAllInvestors();
            var resultData = result.Value as List<InvestorDto>;
            //assert
            Assert.NotNull(result);
            Assert.AreEqual(resultData.Count, data.Count);

        }

        [Test]
        public void GetAllInvestors_Returns_NoRecord()
        {
            //arrange
            var data = new List<InvestorDto>();
            _service.Setup(i => i.GetInvestors()).Returns(data);

            //act
            var result = (NotFoundResult)_controller.GetAllInvestors();
            // var resultData = result.Value as List<InvestorDto>;
            //assert
            Assert.That(404, Is.EqualTo(result.StatusCode));
            //Assert.AreEqual(resultData.Count, data.Count);
        }
    }
}