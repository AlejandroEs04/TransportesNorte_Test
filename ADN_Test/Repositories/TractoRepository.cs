using ADN_Test.Data;
using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    public class TractoRepository : ITractoRepository
    {
        private readonly DataContextDapper _dapper;

        public TractoRepository()
        {
            _dapper = new DataContextDapper();
        }

        public async Task<IEnumerable<Tracto>> GetAllTractos()
        {
            return await _dapper.Query<Tracto>("SELECT * FROM vw_Tracto");
        }
    }
}
