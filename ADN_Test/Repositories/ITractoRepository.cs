using ADN_Test.Dtos;
using ADN_Test.Models;

namespace ADN_Test.Repositories
{
    public interface ITractoRepository
    {
        Task<IEnumerable<Tracto>> GetAllTractos();
        Task<int?> GetByPlacaAsync(string placa);
        Task CreateTractoAsync(CreateTractoDto dto);
    }
}
