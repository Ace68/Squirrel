using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

using Squirrel.Domain.Abstracts;

namespace Squirrel.Domain.Repositories
{
    public interface ISquirrelRepository<TEntity> where TEntity : EntityBase
    {
        ObservableCollection<TEntity> EntityCollection { get; }

        void Insert(TEntity entityToAdd);
        void Update(TEntity entityToUpdate);
        void Delete(Guid id);

        TEntity GetById(Guid id);
        IList<TEntity> GetAll(int pageIndex, int pageSize, string includeProperties = "");
        IList<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
