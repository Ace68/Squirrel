using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Squirrel.Domain.Entities;

namespace Squirrel.Domain.Repositories
{
    public interface IDeviceRepository
    {
        void Insert(Device entityToAdd);
        void Update(Device entityToUpdate);
        void Delete(Guid id);

        Device GetById(Guid id);
        IList<Device> GetAll(int pageIndex, int pageSize);
        IList<Device> Query(Expression<Func<Device, bool>> filter = null,
            Func<IQueryable<Device>, IOrderedQueryable<Device>> orderBy = null);
    }
}
