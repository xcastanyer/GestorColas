using System.ServiceProcess;

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
