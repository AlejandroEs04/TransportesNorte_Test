using ADN_Test.Models;
using ADN_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
        {
            return await _centroOperativoRepository.GetAll();
        }
    }
}
