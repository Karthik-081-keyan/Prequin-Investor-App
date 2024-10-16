using Investor.Domain.Model;
using Investor.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Investor.Infrastructure.Test
{


    namespace Investor.Tests
    {
        [TestFixture]
        public class InvestorRepositoryTest
        {
            private InvestorRepository _repository;
            private DbContextOptions<AppDbContext> _options;
            private AppDbContext dbContext;
            [SetUp]
            public void Setup()
            {
                // Setup an in-memory database for testing
                _options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

                using (var context = new AppDbContext(_options))
                {
                    // Seed the database with test data
                    context.Investors.AddRange(new List<InvestorModel>
                {
                    new InvestorModel { Id = 1, Name = "Investor A", CommitmentAmount = 1000, CommitmentAssetClass = "Equity", DateAdded = DateTime.UtcNow, Country = "USA", Type = "Individual" ,CommitmentCurrency="A"},
                    new InvestorModel { Id = 2, Name = "Investor A", CommitmentAmount = 2000, CommitmentAssetClass = "Bond", DateAdded = DateTime.UtcNow, Country = "USA", Type = "Individual",CommitmentCurrency="A" },
                    new InvestorModel { Id = 3, Name = "Investor B", CommitmentAmount = 1500, CommitmentAssetClass = "Equity", DateAdded = DateTime.UtcNow, Country = "Canada", Type = "Individual",CommitmentCurrency="A" },
                    new InvestorModel { Id = 4, Name = "Investor C", CommitmentAmount = 3000, CommitmentAssetClass = "Real Estate", DateAdded = DateTime.UtcNow, Country = "UK", Type = "Corporate",CommitmentCurrency="A" }
                });

                    context.SaveChanges();
                }

                dbContext = new AppDbContext(_options);
                _repository = new InvestorRepository(dbContext);
            }

            [TearDown]
            public void CleanUp()
            {
                var investors = dbContext.Investors.ToList();
                dbContext.Investors.RemoveRange(investors);
                dbContext.SaveChanges();
                dbContext.Dispose();
            }

            [Test]
            public void GetInvestors_Returns_InvestorData()
            {
                // Act
                var result = _repository.GetInvestors();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count); // Expecting 3 unique investors

            }

            [Test]
            public void GetCommitments_ReturnCommitmentsForInvestor()
            {
                // Act
                var result = _repository.GetCommitments("Investor A");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Commitments.Count);
                Assert.AreEqual(2, result.CommitmentsInfo.Count);
            }

            [Test]
            public void GetCommitments_ShouldReturnEmpty_WhenInvestorNotFound()
            {
                // Act
                var result = _repository.GetCommitments("Nonexistent Investor");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Commitments.Count); // Only "All" should be present
                Assert.AreEqual("All", result.Commitments[0].Type);
            }
        }
    }
}