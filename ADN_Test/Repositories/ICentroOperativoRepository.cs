using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Repositories
{
    public interface ICentroOperativoRepository
    {
        Task<IEnumerable<CentroOperativo>> GetAll();
    }
}
