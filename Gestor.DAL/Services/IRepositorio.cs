using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.DAL.Services
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Update(TEntity entityToUpdate);
    }
}
