using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackendService.Model
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity: class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");
        TEntity GetByID(int ID);
        void Insert(TEntity Entity);
        void Delete(int Id);
        void Update(TEntity Entity);
        void Save();
    }
}
