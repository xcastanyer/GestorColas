using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.DAL.Model
{
    public enum EstadoProceso
    {
        Pendiente,
        EnEjecucion,
        FinalizadoSinErrores,
        FinalizadoConErrores,
        Abortado
    }
}
