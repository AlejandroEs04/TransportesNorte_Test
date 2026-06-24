using ADN_Test.Data;
using ADN_Test.Dtos;
using ADN_Test.Models;
using System.Data;

namespace ADN_Test.Repositories
{
    public class CentroOperativoRepository : ICentroOperativoRepository
    {
        private readonly DataContextDapper _dapper;

        public CentroOperativoRepository()
        {
            _dapper = new DataContextDapper();
        }

        public async Task<IEnumerable<CentroOperativo>> GetAll()
        {
            return await _dapper.Query<CentroOperativo>("SELECT * FROM CentroOperativo");
        }

        public async Task<int?> GetByNombreAsync(string nombre)
        {
            var result = await _dapper.Query<int>(
                "SELECT Id FROM CentroOperativo WHERE Nombre = @Nombre",
                new { Nombre = nombre });
            return result.FirstOrDefault();
        }

        public async Task CreateCentroOperativoAsync(CreateCentroOperativoDto dto)
        {
            await _dapper.Execute("sp_CreateCentroOperativo",
                new { dto.Nombre },
                CommandType.StoredProcedure);
        }
    }
}
