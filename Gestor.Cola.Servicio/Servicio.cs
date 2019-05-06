using Gestor.DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Cola.Servicio
{
    public partial class Servicio : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Servicio()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.Info("Servicio INICIADO");         
            
            EjecutorColas EJ = new EjecutorColas();
            log.Info("Instancia Ejecutor Colas");
            EJ.IniciarColasAll();

        }

        protected override void OnStop()
        {
            log.Info("Servicio PARADO");
        }
    }
}
