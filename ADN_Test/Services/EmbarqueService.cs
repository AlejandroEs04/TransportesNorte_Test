using ADN_Test.Dtos;
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

        public async Task<IEnumerable<EmbarqueResponseDto>> GetAll()
        {
            return await _embarqueRepository.GetAllEmbarques();
        }

        public async Task CreateEmbarque(EmbarqueInsertDto dto)
        {
            await _embarqueRepository.CreateEmbarque(dto);
        }

        public async Task UpdateSalida(EmbarqueUpdateSalida dto)
        {
            await _embarqueRepository.UpdateSalida(dto);
        }
    }
}
