using System.Collections.Generic;

using Squirrel.Domain.Services.Dtos;

namespace Squirrel.Domain.Services.Abstracts
{
    public interface IRoomService
    {
        List<DtoRoom> GetRoomsFromRepository();
        void SaveRoomsIntoRepository(IList<DtoRoom> roomsToSave);
    }
}
