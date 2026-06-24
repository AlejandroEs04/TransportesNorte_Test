using ADN_Test.Dtos;
using ADN_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
        {
            return await _empleadoRepository.GetEmpleadosWithNombre();
        }
    }
}
