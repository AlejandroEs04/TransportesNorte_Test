using ADN_Test.Dtos;
using ADN_Test.Repositories;
using ADN_Test.Services;
using ClosedXML.Excel;
using System.Diagnostics;

namespace ADN_Test.Service
{
    public class EmbarqueService : IEmbarqueService
    {
        private readonly IEmbarqueRepository _embarqueRepository;
        private readonly ITractoService _tractoService;
        private readonly IEmpleadoService _empleadoService;
        private readonly ICentroOperativoService _centroOperativoService;

        public EmbarqueService()
        {
            _embarqueRepository = new EmbarqueRepository();
            _tractoService = new TractoService();
            _empleadoService = new EmpleadoService();
            _centroOperativoService = new CentroOperativoService();
        }

        public async Task<IEnumerable<EmbarqueResponseDto>> GetAll()
            => await _embarqueRepository.GetAllEmbarques();

        public async Task CreateEmbarque(EmbarqueInsertDto dto)
            => await _embarqueRepository.CreateEmbarque(dto);

        public async Task UpdateSalida(EmbarqueUpdateSalida dto)
            => await _embarqueRepository.UpdateSalida(dto);

        public async Task<EmbarqueImportResult> ImportFromExcelAsync(string filePath)
        {
            var result = new EmbarqueImportResult();

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.RangeUsed();
            if (range is null)
                return result;
            var rows = range.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                result.TotalRows++;
                try
                {
                    var centroOperativo = row.Cell(2).GetString().Trim();
                    var placaTracto = row.Cell(3).GetString().Trim();
                    var nombreConductor = row.Cell(4).GetString().Trim();
                    var pesoTaraText = row.Cell(5).GetString().Trim();
                    var pesoTeoricoText = row.Cell(6).GetString().Trim();

                    decimal? pesoTara = null;
                    if (decimal.TryParse(pesoTaraText, out var tara))
                        pesoTara = tara;

                    if (!decimal.TryParse(pesoTeoricoText, out var pesoTeorico))
                        throw new FormatException($"Peso_Teorico_ERP inválido: '{pesoTeoricoText}'");

                    var tractoId = await _tractoService.GetOrCreateTractoAsync(placaTracto, pesoTara);
                    var conductorId = await _empleadoService.GetOrCreateConductorAsync(nombreConductor);
                    var centroId = await _centroOperativoService.GetOrCreateCentroOperativoAsync(centroOperativo);

                    await _embarqueRepository.CreateEmbarque(new EmbarqueInsertDto
                    {
                        Centro_Operativo_Id = centroId,
                        Tracto_Id = tractoId,
                        Conductor_Id = conductorId,
                        Peso_Teorico_ERP = pesoTeorico
                    });

                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    result.ErrorCount++;
                    result.Errors.Add($"Fila {result.TotalRows}: {ex.Message}");
                }
            }

            return result;
        }
    }
}
