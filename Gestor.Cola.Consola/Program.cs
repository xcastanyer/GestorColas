using Gestor.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Cola.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            EjecutorColas EJ = new EjecutorColas();
            EJ.IniciarColasAll();
        }
    }
}
