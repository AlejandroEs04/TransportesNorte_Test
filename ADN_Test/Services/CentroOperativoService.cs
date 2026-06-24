using ADN_Test.Dtos;
using ADN_Test.Models;
using ADN_Test.Repositories;

namespace ADN_Test.Services
{
    public class CentroOperativoService : ICentroOperativoService
    {
        private readonly ICentroOperativoRepository _centroOperativoRepository;

        public CentroOperativoService()
        {
            _centroOperativoRepository = new CentroOperativoRepository();
        }

        public async Task<IEnumerable<CentroOperativo>> GetAll()
            => await _centroOperativoRepository.GetAll();

        public async Task<int> GetOrCreateCentroOperativoAsync(string nombre)
        {
            int? id = await _centroOperativoRepository.GetByNombreAsync(nombre);
            if (id.HasValue && id.Value != 0)
                return id.Value;

            await _centroOperativoRepository.CreateCentroOperativoAsync(
                new CreateCentroOperativoDto { Nombre = nombre });

            id = await _centroOperativoRepository.GetByNombreAsync(nombre);
            return id ?? throw new InvalidOperationException(
                $"No se pudo obtener el Id del centro operativo '{nombre}' tras insertarlo.");
        }
    }
}
