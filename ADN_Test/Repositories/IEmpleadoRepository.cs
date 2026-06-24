using ADN_Test.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<EmpleadoWithNombreDto>> GetEmpleadosWithNombre();
    }
}
