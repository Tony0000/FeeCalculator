using System.Threading.Tasks;

namespace Api.Services
{
    public interface IInterestRateService
    {
        public Task<double> GetInterestRate();
    }
}