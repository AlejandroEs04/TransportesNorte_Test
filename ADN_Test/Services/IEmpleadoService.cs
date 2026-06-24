using ADN_Test.Dtos;

namespace ADN_Test.Services
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoWithNombreDto>> GetEmpleadosWithNombre();
        Task<int> GetOrCreateConductorAsync(string nombreCompleto);
    }
}
