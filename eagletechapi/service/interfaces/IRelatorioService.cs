using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eagletechapi.service.interfaces
{
    public interface IRelatorioService
    {
        Task ChamadosAbertosPorPeriodo(DateTime start, DateTime end);
    }
}