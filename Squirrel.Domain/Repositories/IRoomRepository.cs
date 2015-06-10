using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Squirrel.Domain.Entities;

namespace Squirrel.Domain.Repositories
{
    public interface IRoomRepository
    {
        void Insert(Room entityToAdd);
        void Update(Room entityToUpdate);
        void Delete(Guid id);

        Room GetById(Guid id);
        IList<Room> GetAll(int pageIndex, int pageSize);
        IList<Room> Query(Expression<Func<Room, bool>> filter = null,
            Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null);
    }
}
