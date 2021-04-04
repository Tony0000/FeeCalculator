using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.IntegrationTest.Controllers
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }
    }
}