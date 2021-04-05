using System.Net.Http;
using System.Threading.Tasks;
using Api.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest.Controllers
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IApiOneClient, MoqApiOneClient>();
                });
            }).CreateClient();
            // TestClient = appFactory.CreateClient();
        }

        private class MoqApiOneClient : IApiOneClient
        {
            public Task<double> GetInterestRate()
            {
                return Task.FromResult(0.01);
            }
        }
    }
}