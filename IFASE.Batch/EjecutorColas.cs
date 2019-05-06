using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IFASE.Batch
{
    public class EjecutorColas
    {

        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EjecutorColas()
        {
         
        }

        public void IniciarColasAll()
        {           
                Cola c = new Cola(0,"IFASE_COLA");               
                Thread.Sleep(2000);
                c.Start();           
        }







    }

}
