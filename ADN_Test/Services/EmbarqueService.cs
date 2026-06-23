using ADN_Test.Models;
using ADN_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ADN_Test.Service
{
    public class EmbarqueService : IEmbarqueService
    {
        private readonly IEmbarqueRepository _embarqueRepository;
        
        public EmbarqueService()
        {
            _embarqueRepository = new EmbarqueRepository();
        }

        public async Task<IEnumerable<Embarque>> GetAll()
        {
            return await _embarqueRepository.GetAllEmbarques();
        }

        public async Task UpdateSalida(Embarque embarque)
        {
            if (!embarque.Peso_Bascula_Salida.HasValue || !embarque.Peso_Neto_Real.HasValue)
                throw new InvalidOperationException("Fields are required");

            await _embarqueRepository.UpdateSalida(new Dtos.EmbarqueUpdateSalida
            {
                Id = embarque.Id,
                Peso_Bascula_Salida = embarque.Peso_Bascula_Salida.Value,
                Peso_Neto_Real = embarque.Peso_Neto_Real.Value,
                Justificacion_Diferencia = embarque.Justificacion_Diferencia
            });
        }
    }
}
