using IFASE.DAL.Model;
using IFASE.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IFASE.Batch
{
    public class Cola
    {

        static readonly log4net.ILog log =
                        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string F50_IMPORTACION_PROCESO = "F50.Importacion.Proceso";
        private const string F50_VOLCADO_PROCESO = "F50.Volcado.Proceso";
        private const string ESTADO_COLA_RUN = "RUN";
        private const string SII_PRESENTACION_PROCESO = "SII.Presentacion.Proceso";
        private const string INTERFACE_SII_LFR_PROCESO = "Interface.SII.LFR.Proceso";
        private const string INTERFACE_SII_LFE_PROCESO = "Interface.SII.LFE.Proceso";

        internal void Stop()
        {
            Iniciado = false;
            log.Info($"Petición parada: {ColaId}");
        }

        public string Nombre { get; set; }

        private int RecId = 0;
        private string ColaId = "";
        List<TrabajosBatch> TrabajosActivos = new List<TrabajosBatch>();


        public Cola(int trabajoId, string colaId)
        {

            RecId = trabajoId;
            ColaId = colaId;
        }

        bool Iniciado;
        public async void Start()
        {
            TrabajoBatchRepositorio rep = new TrabajoBatchRepositorio();

            Iniciado = true;
            log.Info($" Inicio cola: {ColaId} ");
            while (Iniciado)
            {
                Thread.Sleep(1000);
                await Task.Run(() =>
                {
                    try
                    {
                        var trabajoActivo = rep.ObtenerSiguieteTrabajo();

                        rep.MarcarTrabajoCorrecto(trabajoActivo);
                        //var trabajoBatch = rep.ObtenerSiguieteTrabajo();
                       // rep.MarcarTrabajoComoFinalizado(trabajoBatch);

                        //switch (trabajoBatch?.TransaccionId ?? "")
                        //{
                        //    case F50_IMPORTACION_PROCESO:

                        //        F50ImportacionProceso procesoImportacion = new F50ImportacionProceso(RecId, trabajoBatch);
                        //        try
                        //        {
                        //            procesoImportacion.Ejecutar();

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            trabajoBatch = ActualizarTrabajoFinNoControlado(trabajoBatch, ex);
                        //            foreach (var empresa in procesoImportacion.paramEmpresas)
                        //            {
                        //                procesoImportacion.EnviarNotificacion(empresa, 0, TrabajoEstadoClaseEnum.FinError, ex.Message);
                        //            }
                        //        }
                        //        break;
                        //    case F50_VOLCADO_PROCESO:
                        //        F50VolcadoProceso procesoVolcado = new F50VolcadoProceso(RecId, trabajoBatch);
                        //        try
                        //        {
                        //            procesoVolcado.Ejecutar();
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            trabajoBatch = ActualizarTrabajoFinNoControlado(trabajoBatch, ex);
                        //            foreach (var empresa in procesoVolcado.paramEmpresas)
                        //            {
                        //                procesoVolcado.EnviarNotificacion(empresa, 0, TrabajoEstadoClaseEnum.FinError, ex.Message);
                        //            }

                        //        }
                        //        break;
                        //    case SII_PRESENTACION_PROCESO:
                        //        try
                        //        {
                        //            SIIPresentacionProceso procesoPresentacion = new SIIPresentacionProceso(trabajoBatch.RecId);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            trabajoBatch = ActualizarTrabajoFinNoControlado(trabajoBatch, ex);
                        //        }

                        //        break;
                        //    case INTERFACE_SII_LFE_PROCESO:
                        //        try
                        //        {
                        //            InterfaceLFESIIProceso procesoInterfaceLFEsii = new InterfaceLFESIIProceso(trabajoBatch.RecId);
                        //            SIIPresentacionProceso procesoPresentacion = new SIIPresentacionProceso(trabajoBatch.RecId);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            trabajoBatch = ActualizarTrabajoFinNoControlado(trabajoBatch, ex);
                        //        }
                        //        break;
                        //    case INTERFACE_SII_LFR_PROCESO:
                        //        try
                        //        {
                        //            InterfaceLFRSIIProceso procesoInterfaceLFRsii = new InterfaceLFRSIIProceso(trabajoBatch.RecId);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            trabajoBatch = ActualizarTrabajoFinNoControlado(trabajoBatch, ex);
                        //        }
                        //        break;
                        //    default:
                        //        if ((trabajoBatch?.TransaccionId ?? "") != "")
                        //        {
                        //            trabajoBatch = srvTrabajo.GetTrabajoBatchByRecId(RecId, trabajoBatch.RecId);
                        //            trabajoBatch.EstadoClase = Infraestructura.DL.Enums.TrabajoEstadoClaseEnum.FinError.ToString();
                        //            srvTrabajo.Modificar(RecId, trabajoBatch);
                        //            srvTrabajo.AnotarAgenda(trabajoBatch.RecId, AgendaClaseEnum.Usuario.ToString(), $"No se encuentra ejecutor para  el trabajo {trabajoBatch?.TransaccionId}");
                        //        }
                        //        break;
                        //}

                    }
                    catch (Exception ex)
                    {
                        log.Fatal($"IFASE_SAP Método start: {ex.Message}");
                    }
                });
            }
        }

       
    }
}
