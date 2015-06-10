using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

using Squirrel.Domain.Entities;
using Squirrel.Domain.Repositories;
using Squirrel.Persistence.Facade;

namespace Squirrel.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ObservableCollection<Room> _roomCollection;

        public RoomRepository(DomainModelFacade domainModelFacade)
        {
            this._roomCollection = domainModelFacade.Room;
        }

        public void Insert(Room entityToAdd)
        {
            if (entityToAdd == null) return;

            this._roomCollection.Add(entityToAdd);
        }

        public void Update(Room entityToUpdate)
        {
            if (entityToUpdate == null) return;

            this.Delete(entityToUpdate.Id);

            this._roomCollection.Add(entityToUpdate);
        }

        public void Delete(Guid id)
        {
            var currentEntity = this.GetById(id);

            if (currentEntity != null)
                this._roomCollection.Remove(currentEntity);
        }

        public Room GetById(Guid id)
        {
            return this._roomCollection.FirstOrDefault(e => e.Id == id);
        }

        public IList<Room> GetAll(int pageIndex, int pageSize)
        {
            var query = this._roomCollection.AsQueryable();

            if (pageSize > 0)
                query = query.OrderBy(q => q.Id).Skip(pageIndex * pageSize).Take(pageSize);

            return query.ToList();
        }

        public IList<Room> Query(Expression<Func<Room, bool>> filter = null,
            Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null)
        {
            var query = this._roomCollection.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }
    }
}
