using ADN_Test.Dtos;
using ADN_Test.Repositories;
using System.Diagnostics;

namespace ADN_Test.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService()
        {
            _empleadoRepository = new EmpleadoRepository();
        }

        public async Task<IEnumerable<EmpleadoWithNombreDto>> GetEmpleadosWithNombre()
            => await _empleadoRepository.GetEmpleadosWithNombre();

        public async Task<int> GetOrCreateConductorAsync(string nombreCompleto)
        {
            int? id = await _empleadoRepository.GetConductorByNombreAsync(nombreCompleto);

            if (id.HasValue && id.Value != 0)
                return id.Value;

            var maxNo = await _empleadoRepository.GetMaxNoEmpleadoAsync();
            var nextNo = (maxNo.GetValueOrDefault() + 1).ToString("D6");

            await _empleadoRepository.CreateConductorAsync(new CreateConductorDto
            {
                Nombre_Completo = nombreCompleto,
                No_Empleado = nextNo,
                Posicion_Id = 2
            });

            id = await _empleadoRepository.GetConductorByNombreAsync(nombreCompleto);
            return id ?? throw new InvalidOperationException(
                $"No se pudo obtener el Id del conductor '{nombreCompleto}' tras insertarlo.");
        }
    }
}
