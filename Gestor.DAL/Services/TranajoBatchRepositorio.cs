using Gestor.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.DAL.Services
{
    public class TrabajoBatchRepositorio
    {
        public async Task<List<TrabajosBatch>> ObtenerTodosAsync()
        {
            List<TrabajosBatch> trabajos = null;
            using (var context = new IFaseDbContext())
            {
                trabajos = await (context.TrabajosBatch.Where(p => p.Estado == EstadoProceso.Pendiente).ToListAsync<TrabajosBatch>());
            }
            return trabajos;
        }
        
        public TrabajosBatch ObtenerSiguieteTrabajo(string nombreCola)
        {
            TrabajosBatch trabajo = null;
            using (var context = new IFaseDbContext())
            {
                trabajo = context.TrabajosBatch.Where(p => p.IdCola == nombreCola && p.Estado == EstadoProceso.Pendiente).FirstOrDefaultAsync<TrabajosBatch>().Result;
            }
            return trabajo;
        }
        public void MarcarTrabajoCorrecto(TrabajosBatch trabajoBatch)
        {
            MarcarTrabajo(trabajoBatch, EstadoProceso.FinalizadoSinErrores);
        }
        public void MarcarTrabajoAbortado(TrabajosBatch trabajoBatch)
        {
            MarcarTrabajo(trabajoBatch, EstadoProceso.Abortado);
        }
        public void MarcarTrabajoErroneo(TrabajosBatch trabajoBatch)
        {
            MarcarTrabajo(trabajoBatch, EstadoProceso.FinalizadoConErrores);
        }
        public void MarcarTrabajoEnEjecucion(TrabajosBatch trabajoBatch)
        {
            MarcarTrabajo(trabajoBatch, EstadoProceso.EnEjecucion);
        }

        public void InsertTrabajoTest()
        {

            using (var context = new IFaseDbContext())
            {
                context.TrabajosBatch.Add(new TrabajosBatch
                {
                    Descripcion = "Test",
                    Estado = EstadoProceso.Pendiente,
                    FechaInsercion = DateTime.Now,
                    FechaInicio = DateTime.Now,
                    IdOrigen = "XXX",
                    IdProceso = "XXXXX",
                    IdClase = "DOCUMENTOS"


                });
                GuardarCambios("No se ha podido insertar el trabajo.", context);

            }

        }
        private void MarcarTrabajo(TrabajosBatch trabajoBatch, EstadoProceso estado)
        {
            trabajoBatch.Estado = estado;
            trabajoBatch.FechaFinalizacion = DateTime.Now;

            using (var context = new IFaseDbContext())
            {
                context.TrabajosBatch.Attach(trabajoBatch);
                context.Entry(trabajoBatch).State = EntityState.Modified;
                GuardarCambios($"No se ha podido marcar el trabajo como {estado.ToString()}.", context);
            }
        }

     

        private void GuardarCambios(string cabeceraMensajeEnCasoDeError, IFaseDbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw new Exception($"{cabeceraMensajeEnCasoDeError}. Causa:\n{LeerExcepcionDbEntityValidationException(ex)} ");
            }
            catch (Exception ex)
            {
                throw new Exception($"{cabeceraMensajeEnCasoDeError}.", ex);
            }
        }
        private string LeerExcepcionDbEntityValidationException(DbEntityValidationException ex)
        {
            string errores = "";
            foreach (var erroresValidacion in ex.EntityValidationErrors)
            {
                foreach (var error in erroresValidacion.ValidationErrors)
                {
                    errores += $"Error en propiedad {error.PropertyName}: {error.ErrorMessage}\n";
                }
            }
            return errores;
        }     
    }
}
