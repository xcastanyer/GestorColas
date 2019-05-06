using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.DAL.Model
{
    public class IFaseDbContext : DbContext
    {
        public IFaseDbContext() : base("IfaseSap")
        {

        }
        public DbSet<TrabajosBatch> TrabajosBatch { get; set; }
    }
}
