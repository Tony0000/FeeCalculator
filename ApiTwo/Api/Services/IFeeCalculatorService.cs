using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IFeeCalculatorService
    {
        public Task<double> CalculateFees(FeeQuery query);
        public Task<string> GetGithubUrl();
    }
}