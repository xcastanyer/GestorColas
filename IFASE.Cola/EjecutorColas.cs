using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gestor.Cola
{
    public class EjecutorColas
    {

        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EjecutorColas()
        {
         
        }

        public void IniciarColasAll()
        {          
            ColaSection seccionCola = ConfigurationManager.GetSection("gestorColas") as ColaSection;
            int numColas = seccionCola.Colas.Count;
            log.Info($"Hay {numColas.ToString()} cola/s en la sección gestorColas de fichero .config");
            foreach (var cola in seccionCola.Colas)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart((cola as Cola).Start));

                thread.Start();
                Thread.Sleep(1000);
            }
                  
        }







    }

}
