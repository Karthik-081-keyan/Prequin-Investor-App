using Investor.Application.Services;
using Investor.Domain.DTO;
using InvestorApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InvestApp.Test.Controller
{

    public class CommitmentControllerTest
    {
        private CommitmentController _controller;
        private Mock<ICommitmentService> _service;
        public CommitmentControllerTest()
        {
            _service = new Mock<ICommitmentService>();
            _controller = new CommitmentController(_service.Object);
        }

        [OneTimeTearDown]
        public void cleanUp()
        {
            _controller.Dispose();
            _controller.Dispose();
        }

        [Test]
        public void GetAllInvestors_Returns_CommitmentInfo()
        {
            //arrange
            var data = new CommitmentsDto()
            {
                Commitments = Enumerable.Range(0, 2)
                        .Select(i => new Commitment() { Type = i.ToString() })
                        .ToList()

            };
            _service.Setup(i => i.GetCommitments(It.IsAny<string>())).Returns(data);

            //act
            var result = (OkObjectResult)_controller.GetCommitmentDetails("test");
            var resultData = result.Value as CommitmentsDto;
            //assert
            Assert.NotNull(result);
            Assert.AreEqual(resultData.Commitments.Count, 2);

        }

        [Test]
        public void GetAllInvestors_Returns_BadRequest()
        {
            //arrange
            //act
            var result = (BadRequestObjectResult)_controller.GetCommitmentDetails(string.Empty);
            //assert
            Assert.That(400, Is.EqualTo(result.StatusCode));
            Assert.That(result.Value, Is.EqualTo("Investor Name cannot be empty"));
        }
    }
}
