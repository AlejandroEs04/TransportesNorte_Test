using ADN_Test.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Services
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoWithNombreDto>> GetEmpleadosWithNombre();
    }
}
