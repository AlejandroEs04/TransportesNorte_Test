using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Service
{
    public interface IEmbarqueService
    {
        Task<IEnumerable<Embarque>> GetAll();
        Task UpdateSalida(Embarque embarque);
    }
}
