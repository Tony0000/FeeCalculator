using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Api.Common
{
    public class ApiOneClient : IApiOneClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _settings;

        public ApiOneClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<double> GetInterestRate()
        {
            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            var interestRate = await _httpClient.GetStringAsync(_settings.ApiOneHost+"/taxaJuros");
            
            return Convert.ToDouble(interestRate, provider);
        }
    }
}