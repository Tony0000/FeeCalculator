using System.Threading.Tasks;
using Api.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace Api.IntegrationTest.Controllers
{
    public class FeeCalculatorControllerTest : IntegrationTest
    {
        [Test]
        public async Task ShowMeTheCode_ShouldReturnRepositoryUrl_WhenReached()
        {
            // act
            var result = await TestClient.GetAsync(ApiRoutes.FeeCalculator.ShowMeTheCode);
            
            // assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            var content = await result.Content.ReadAsStringAsync();
            content.Should().Be($"\"{StaticUrls.GithubRepository}\"");
        }

        [Test]
        public async Task CalculateFee_ShouldReturnBadRequest_WhenNoFieldValuesAreProvided()
        {
            // act
            var result = await TestClient.GetAsync(ApiRoutes.FeeCalculator.CalcFee);

            // assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        
        [Test]
        public async Task CalculateFee_ShouldReturnBadRequest_WhenFieldValuesAreInvalid()
        {
            // arrange
            var queryParams = "/?initialValue=100&months=-1";
            
            // act
            var result = await TestClient.GetAsync(ApiRoutes.FeeCalculator.CalcFee + queryParams);

            // assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}