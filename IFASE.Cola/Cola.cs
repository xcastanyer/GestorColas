using Gestor.DAL.Model;
using Gestor.DAL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Gestor.Cola
{
    public class Cola : ConfigurationElement
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

      
        [ConfigurationProperty("nombreCola", IsRequired = true)]
        public string NombreCola
        {
            get
            {
                return this["nombreCola"] as string;
            }
        }
        [ConfigurationProperty("path", IsRequired = true)]
        public string PathEjecutable
        {
            get
            {
                return this["path"] as string;
            }
        }
        [ConfigurationProperty("ejecutable", IsRequired = true)]
        public string NombreEjecutable
        {
            get
            {
                return this["ejecutable"] as string;
            }
        }
        [ConfigurationProperty("timeOut",DefaultValue = 100000)]
        public int TimeOut
        {
            get
            {
                int valor = 0;
                if (int.TryParse(this["timeOut"].ToString(), out valor))
                    return valor;
                else
                    return 100000;

            }
        }


        List<TrabajosBatch> TrabajosActivos = new List<TrabajosBatch>();

       
        public Cola()
        {
            
        }

        bool Iniciado;
        public void Start()
        {
            TrabajoBatchRepositorio rep = new TrabajoBatchRepositorio();

            Iniciado = true;
            log.Info($"Inicio cola: {NombreCola} ");
            while (Iniciado)
            {
               
                Thread.Sleep(2000);

                try
                {
                    var tb = rep.ObtenerSiguieteTrabajo(NombreCola);
                    if (tb != null)
                    {
                        log.Info($"Trabajo iniciado en cola {NombreCola}");
                        rep.MarcarTrabajoEnEjecucion(tb);
                        //Thread.Sleep(5000);
                        string ParametrosLlamada = $"{tb.IdClase}-{tb.IdOrigen}-{tb.IdDocumentoInterno}-{tb.IdSociedad}-{tb.IdTipoEjecucionSAP}";
                        string Ejecutor = Path.Combine(PathEjecutable, NombreEjecutable);

                        if (ParametrosLlamada == "----")
                            ParametrosLlamada = tb.ParametrosEx;

                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.WorkingDirectory = PathEjecutable;
                        Console.WriteLine(ParametrosLlamada);
                        psi.Arguments = ParametrosLlamada;
                        psi.FileName = Ejecutor;

                        log.Info($"{NombreCola} - Se realiza la siguiente llamada : {Ejecutor} {ParametrosLlamada}");
                   
                        try
                        {
                            Process p = Process.Start(psi);
                            //p.WaitForExit(1000000);
                            p.WaitForExit(TimeOut);
                            //Comprueba si el proceso sigue ejecutándose.
                            if (p.HasExited == false)
                            {
                                //El proceso sigue ejecutándose.
                                //Comprueba si el proceso está bloqueado.
                                if (p.Responding)
                                {
                                    //El proceso estaba respondiendo; cerrar la ventana principal.
                                    log.Fatal($"{NombreCola} - Timeout excedido. ejecutando {p.Modules[0].FileName}. Agregue o modifique el atributo timeOut con un valor superior a {TimeOut.ToString()} para la cola {NombreCola} en el archivo .Config.");
                                    p.CloseMainWindow();
                                    p.Kill();
                                    rep.MarcarTrabajoAbortado(tb);
                                }
                                else
                                {
                                    //El proceso no estaba respondiendo; forzar el cierre del proceso.
                                    log.Fatal($"{NombreCola} - Error ejecutando {p.Modules[0].FileName}");
                                    p.Kill();
                                    rep.MarcarTrabajoAbortado(tb);
                                }
                            }
                            else
                            {
                                rep.MarcarTrabajoCorrecto(tb);
                                log.Info($"{NombreCola} - Trabajo finalizado correctamente");
                            }
                         
                        }
                        
                        catch (Exception ex)
                        {
                            rep.MarcarTrabajoErroneo(tb);
                            log.Fatal($"{NombreCola} - ERROR al intentar ejecutar:\n   {Ejecutor} ", ex);
                        }
                        log.Info($"{NombreCola} - Esperando siguiente trabajo...");
                    }



                }
                catch (Exception ex)
                {

                    log.Fatal($"ERROR. Cola {NombreCola} finalizada por el siguiente error:", ex);

                }

            }
        }

        internal void Stop()
        {
            Iniciado = false;
            log.Info($"Petición parada: {NombreCola}");
        }


    }



}
