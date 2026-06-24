using ADN_Test.Dtos;
using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    internal interface IEmbarqueRepository
    {
        Task<IEnumerable<EmbarqueResponseDto>> GetAllEmbarques();
        Task CreateEmbarque(EmbarqueInsertDto dto);
        Task UpdateSalida(EmbarqueUpdateSalida dto);
    }
}
