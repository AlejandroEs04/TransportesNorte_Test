using ADN_Test.Data;
using ADN_Test.Dtos;
using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly DataContextDapper _dapper;

        public EmpleadoRepository()
        {
            _dapper = new DataContextDapper();
        }

        public async Task<IEnumerable<EmpleadoWithNombreDto>> GetEmpleadosWithNombre()
        {
            return await _dapper.Query<EmpleadoWithNombreDto>("SELECT * FROM vw_EmpleadoWithNombre");
        }
    }
}
