using ADN_Test.Dtos;
using ADN_Test.Models;
using ADN_Test.Repositories;
using System.Diagnostics;

namespace ADN_Test.Services
{
    public class TractoService : ITractoService
    {
        private readonly ITractoRepository _tractoRepository;

        public TractoService()
        {
            _tractoRepository = new TractoRepository();
        }

        public async Task<IEnumerable<Tracto>> GetAllTractos()
            => await _tractoRepository.GetAllTractos();

        public async Task<int> GetOrCreateTractoAsync(string placa, decimal? pesoTara)
        {
            int? id = await _tractoRepository.GetByPlacaAsync(placa);

            if (id.HasValue && id.Value != 0)
                return id.Value;

            await _tractoRepository.CreateTractoAsync(new CreateTractoDto
            {
                Placa_Tracto = placa,
                Peso_Tara = pesoTara
            });

            id = await _tractoRepository.GetByPlacaAsync(placa);
            return id ?? throw new InvalidOperationException(
                $"No se pudo obtener el Id del tracto con placa '{placa}' tras insertarlo.");
        }
    }
}
