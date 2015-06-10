using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Squirrel.Domain.Abstracts;
using Squirrel.Domain.Entities;
using Squirrel.Domain.Repositories;
using Squirrel.Persistence.Facade;

namespace Squirrel.Persistence.Repositories
{
    internal class SquirrelRepository<TEntity> : SquirrelRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly DomainModelFacade _domainModelFacade;

        public override ObservableCollection<TEntity> EntityCollection { get; set; }

        public SquirrelRepository(DomainModelFacade domainModelFacade)
        {
            this._domainModelFacade = domainModelFacade;
        }


        public override void Insert(TEntity entityToAdd)
        {
            if (this.EntityCollection == null) return;

            this.EntityCollection.Add(entityToAdd);
        }

        public override void Update(TEntity entityToUpdate)
        {
            if (this.EntityCollection == null) return;

            var currentEntity = this.EntityCollection.FirstOrDefault(e => e.Id == entityToUpdate.Id);

            if (currentEntity == null) return;

            this.EntityCollection.Remove(currentEntity);
            this.EntityCollection.Add(entityToUpdate);
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IList<TEntity> GetAll(int pageIndex, int pageSize, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public override IList<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }
    }
}
