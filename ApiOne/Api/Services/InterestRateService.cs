using System.Threading.Tasks;

namespace Api.Services
{
    public class InterestRateService: IInterestRateService
    {
        public async Task<double> GetInterestRate()
        {
            return await Task.FromResult(0.01);
        }
    }
}