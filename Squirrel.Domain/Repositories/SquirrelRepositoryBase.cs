using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

using Squirrel.Domain.Abstracts;

namespace Squirrel.Domain.Repositories
{
    public abstract class SquirrelRepositoryBase<TEntity> : ISquirrelRepository<TEntity> where TEntity : EntityBase
    {
        public abstract ObservableCollection<TEntity> EntityCollection { get; set; }

        public abstract void Insert(TEntity entityToAdd);

        public abstract void Update(TEntity entityToUpdate);

        public abstract void Delete(Guid id);

        public abstract TEntity GetById(Guid id);


        public abstract IList<TEntity> GetAll(int pageIndex, int pageSize, string includeProperties = "");

        public abstract IList<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
    }
}
