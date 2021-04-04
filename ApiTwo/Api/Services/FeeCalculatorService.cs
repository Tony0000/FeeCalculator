using System;
using System.Threading.Tasks;
using Api.Common;
using Api.Models;

namespace Api.Services
{
    public class FeeCalculatorService : IFeeCalculatorService
    {
        private readonly IApiOneClient _httpClient;
        
        public FeeCalculatorService(IApiOneClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> CalculateFees(FeeQuery query)
        {
            var interestRate = await _httpClient.GetInterestRate();
            var result = (double) (query.InitialValue * 
                                   Math.Pow((1 + interestRate), (double) query.Months));
            
            return Math.Truncate(result * 100) / 100;
        }

        public async Task<string> GetGithubUrl()
        {
            return await Task
                .FromResult(StaticUrls.GithubRepository);
        }
    }
}