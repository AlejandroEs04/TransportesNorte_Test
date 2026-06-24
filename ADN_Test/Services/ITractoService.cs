using ADN_Test.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADN_Test.Services
{
    public interface ITractoService
    {
        Task<IEnumerable<Tracto>> GetAllTractos();
    }
}
