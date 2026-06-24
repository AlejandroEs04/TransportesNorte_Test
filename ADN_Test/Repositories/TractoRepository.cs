using ADN_Test.Data;
using ADN_Test.Dtos;
using ADN_Test.Models;
using System.Data;

namespace ADN_Test.Repositories
{
    public class TractoRepository : ITractoRepository
    {
        private readonly DataContextDapper _dapper;

        public TractoRepository()
        {
            _dapper = new DataContextDapper();
        }

        public async Task<IEnumerable<Tracto>> GetAllTractos()
        {
            return await _dapper.Query<Tracto>("SELECT * FROM vw_Tracto");
        }

        public async Task<int?> GetByPlacaAsync(string placa)
        {
            var result = await _dapper.Query<int>(
                "SELECT Id FROM Tracto WHERE Placa_Tracto = @Placa",
                new { Placa = placa });
            return result.FirstOrDefault();
        }

        public async Task CreateTractoAsync(CreateTractoDto dto)
        {
            await _dapper.Execute("sp_CreateTracto",
                new { dto.Placa_Tracto, dto.Peso_Tara },
                CommandType.StoredProcedure);
        }
    }
}
