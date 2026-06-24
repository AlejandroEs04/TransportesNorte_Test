using ADN_Test.Models;

namespace ADN_Test.Services
{
    public interface ICentroOperativoService
    {
        Task<IEnumerable<CentroOperativo>> GetAll();
        Task<int> GetOrCreateCentroOperativoAsync(string nombre);
    }
}
