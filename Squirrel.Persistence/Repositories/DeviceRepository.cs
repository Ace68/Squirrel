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
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ObservableCollection<Device> _deviceCollection;

        public DeviceRepository(DomainModelFacade domainModelFacade)
        {
            this._deviceCollection = domainModelFacade.Device;
        }

        public void Insert(Device entityToAdd)
        {
            if (entityToAdd == null) return;

            this._deviceCollection.Add(entityToAdd);
        }

        public void Update(Device entityToUpdate)
        {
            if (entityToUpdate == null) return;

            this.Delete(entityToUpdate.Id);

            this._deviceCollection.Add(entityToUpdate);
        }

        public void Delete(Guid id)
        {
            var currentEntity = this.GetById(id);

            if (currentEntity != null)
                this._deviceCollection.Remove(currentEntity);
        }

        public Device GetById(Guid id)
        {
            return this._deviceCollection.FirstOrDefault(e => e.Id == id);
        }

        public IList<Device> GetAll(int pageIndex, int pageSize)
        {
            var query = this._deviceCollection.AsQueryable();

            if (pageSize > 0)
                query = query.OrderBy(q => q.Id).Skip(pageIndex * pageSize).Take(pageSize);

            return query.ToList();
        }

        public IList<Device> Query(Expression<Func<Device, bool>> filter = null, 
            Func<IQueryable<Device>, IOrderedQueryable<Device>> orderBy = null)
        {
            var query = this._deviceCollection.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }
    }
}
