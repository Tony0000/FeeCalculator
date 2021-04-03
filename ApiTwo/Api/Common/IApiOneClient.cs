using System.Threading.Tasks;

namespace Api.Common
{
    public interface IApiOneClient
    {
        Task<double> GetInterestRate();
    }
}