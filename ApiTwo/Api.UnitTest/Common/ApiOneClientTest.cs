using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Api.Common;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ApiTest.Common
{
    public class ApiOneClientTest
    {
        private ApiOneClient _sut;
        private const string BaseUrl = "http://localhost"; 
        
        [SetUp]
        public void SetupSystemUnderTest()
        {
            var factory = new MockRepository(MockBehavior.Loose);
            var moqOptions = factory.Create<IOptions<ServiceSettings>>();
            moqOptions.Setup(x => x.Value).Returns(new ServiceSettings {ApiOneHost = BaseUrl});

            var Url = BaseUrl + ApiRoutes.ApiOne.TaxaJuros;

            var moqClient = new HttpClient(new HttpMessageHandlerStub());
            _sut = new ApiOneClient(moqClient, moqOptions.Object);
        }
        
        [Test]
        public async Task GetInterestRate_ShouldReturnInterestRate_WhenCalled()
        {
            // act 
            var result = await _sut.GetInterestRate();

            // assert
            result.Should().Be(0.01);
        }

        private class HttpMessageHandlerStub : HttpMessageHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("0.01")
                };

                return await Task.FromResult(response);
            }
        }
    }
}