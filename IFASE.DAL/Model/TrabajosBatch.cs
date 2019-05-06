using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.DAL.Model
{
    public class TrabajosBatch
    {
        public int Id { get; set; }
        public string IdCola { get; set; }
    
        [StringLength(20)]
        public string IdClase { get; set; }    //DOCUMENTOS - ASIENTOS

        [StringLength(20)]
        public string IdOrigen { get; set; }   //CIT - CEX - etc...
        [StringLength(4)]
        public string IdSociedad { get; set; }
        [StringLength(60)]
        public string IdDocumentoInterno { get; set; }
        [StringLength(1)]
        public string IdTipoEjecucionSAP { get; set; } //' ' - '2'
       
        [StringLength(100)]
        public string IdProceso { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required]
        public EstadoProceso Estado { get; set;}
        [Required]
        public DateTime FechaInsercion { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string ParametrosEx { get; set; }

    }
}
