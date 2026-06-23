using ADN_Test.Data;
using ADN_Test.Dtos;
using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    public class EmbarqueRepository : IEmbarqueRepository
    {
        private readonly DataContextDapper _dapper;

        public EmbarqueRepository()
        {
            _dapper = new DataContextDapper();
        }

        public async Task<IEnumerable<Embarque>> GetAllEmbarques()
        {
            return await _dapper.Query<Embarque>("SELECT * FROM Embarque");
        }

        public async Task UpdateSalida(EmbarqueUpdateSalida embarque)
        {
            await _dapper.Execute("sp_UpdateEmbarque_Salida", 
                new { embarque.Id, embarque.Peso_Bascula_Salida, embarque.Peso_Neto_Real, embarque.Justificacion_Diferencia });
        }
    }
}
