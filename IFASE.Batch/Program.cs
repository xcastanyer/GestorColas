using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFASE.Batch
{
    class Program
    {
        static void Main(string[] args)
        {

            IFASE.DAL.Services.TrabajoBatchRepositorio Trabajos = new DAL.Services.TrabajoBatchRepositorio();
            Trabajos.InsertTrabajoTest();
            EjecutorColas EJ = new EjecutorColas();
            EJ.IniciarColasAll();
        }
    }
}
