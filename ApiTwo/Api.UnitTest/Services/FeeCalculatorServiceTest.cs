using System.Threading.Tasks;
using Api.Common;
using Api.Models;
using Api.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ApiTest.Services
{
    [TestFixture]
    public class FeeCalculatorServiceTest
    {
        private FeeCalculatorService _sut;
        private MockRepository _factory;
        
        [SetUp]
        public async Task SetupSystemUnderTest()
        {
            _factory = new(MockBehavior.Loose);
            var httpClient = _factory.Create<IApiOneClient>();
            httpClient.Setup(x => x.GetInterestRate()).ReturnsAsync(await Task.FromResult(0.01));
            
            _sut = new FeeCalculatorService(httpClient.Object);
        }

        [Test]
        public async Task CalculateFees_ShouldReturnFee_WhenInputIsValid()
        {
            // arrange
            var query = new FeeQuery {Months = 5, InitialValue = 100};

            // act
            var result = await _sut.CalculateFees(query);
            
            // assert
            result.Should().Be(105.10);
        }
        
        [Test]
        public async Task GetGithubUrl_ShouldReturnCorrectUrl_WhenCalled()
        {
            // act
            var result = await _sut.GetGithubUrl();
            
            // assert
            result.Should().BeEquivalentTo(StaticUrls.GithubRepository);
        }
    }
}