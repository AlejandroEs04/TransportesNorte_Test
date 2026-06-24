using ADN_Test.Dtos;

namespace ADN_Test.Repositories
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<EmpleadoWithNombreDto>> GetEmpleadosWithNombre();
        Task<int?> GetConductorByNombreAsync(string nombreCompleto);
        Task<int?> GetMaxNoEmpleadoAsync();
        Task CreateConductorAsync(CreateConductorDto dto);
    }
}
