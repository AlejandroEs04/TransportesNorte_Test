using ADN_Test.Models;
using ADN_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Services
{
    public class TractoService : ITractoService
    {
        private readonly TractoRepository _tractoRepository;

        public TractoService()
        {
            _tractoRepository = new TractoRepository();
        }

        public async Task<IEnumerable<Tracto>> GetAllTractos()
        {
            return await _tractoRepository.GetAllTractos();
        }
    }
}
