using ADN_Test.Data;
using ADN_Test.Dtos;
using System.Data;
using System.Diagnostics;

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

        public async Task<int?> GetConductorByNombreAsync(string nombreCompleto)
        {
            var result = await _dapper.Query<int>(
                "SELECT Id FROM Empleado WHERE Nombre_Completo = @Nombre",
                new { Nombre = nombreCompleto });
            return result.FirstOrDefault();
        }

        public async Task<int?> GetMaxNoEmpleadoAsync()
        {
            var result = await _dapper.Query<int>(
                "SELECT ISNULL(MAX(CAST(No_Empleado AS INT)), 0) FROM Empleado WHERE ISNUMERIC(No_Empleado) = 1");
            return result.FirstOrDefault();
        }

        public async Task CreateConductorAsync(CreateConductorDto dto)
        {
            await _dapper.Execute("sp_CreateConductor",
                new { dto.Nombre_Completo, dto.No_Empleado, dto.Posicion_Id },
                CommandType.StoredProcedure);
        }
    }
}
