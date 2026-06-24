using ADN_Test.Data;
using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    public class CentroOperativoRepository : ICentroOperativoRepository
    {
        private readonly DataContextDapper _dapper;

        public CentroOperativoRepository()
        {
            _dapper = new DataContextDapper();
        }

        public async Task<IEnumerable<CentroOperativo>> GetAll()
        {
            return await _dapper.Query<CentroOperativo>("SELECT * FROM CentroOperativo");
        }
    }
}
