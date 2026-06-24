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

        public async Task<IEnumerable<EmbarqueResponseDto>> GetAllEmbarques()
        {
            return await _dapper.Query<EmbarqueResponseDto>("SELECT * FROM vw_Embarque");
        }

        public async Task CreateEmbarque(EmbarqueInsertDto embarque)
        {
            await _dapper.Execute("sp_CreateEmbarque", 
                new { embarque.Centro_Operativo_Id, embarque.Tracto_Id, embarque.Conductor_Id, embarque.Peso_Teorico_ERP });
        }

        public async Task UpdateSalida(EmbarqueUpdateSalida embarque)
        {
            await _dapper.Execute("sp_UpdateEmbarque_Salida", 
                new { embarque.Id, embarque.Peso_Bascula_Salida, embarque.Peso_Neto_Real, embarque.Justificacion_Diferencia });
        }
    }
}
