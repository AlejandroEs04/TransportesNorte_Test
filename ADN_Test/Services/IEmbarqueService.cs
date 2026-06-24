using ADN_Test.Dtos;
using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Service
{
    public interface IEmbarqueService
    {
        Task<IEnumerable<EmbarqueResponseDto>> GetAll();
        Task CreateEmbarque(EmbarqueInsertDto dto);
        Task UpdateSalida(EmbarqueUpdateSalida dto);
        Task<EmbarqueImportResult> ImportFromExcelAsync(string filePath);
    }
}
