using ADN_Test.Dtos;
using ADN_Test.Models;

namespace ADN_Test.Repositories
{
    public interface ICentroOperativoRepository
    {
        Task<IEnumerable<CentroOperativo>> GetAll();
        Task<int?> GetByNombreAsync(string nombre);
        Task CreateCentroOperativoAsync(CreateCentroOperativoDto dto);
    }
}
