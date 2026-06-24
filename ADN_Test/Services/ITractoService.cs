using ADN_Test.Models;

namespace ADN_Test.Services
{
    public interface ITractoService
    {
        Task<IEnumerable<Tracto>> GetAllTractos();
        Task<int> GetOrCreateTractoAsync(string placa, decimal? pesoTara);
    }
}
