using Squirrel.Domain.Entities;

namespace Squirrel.Domain.Repositories
{
    public interface ISquirrelUnitofWork
    {
        ISquirrelRepository<Device> DeviceRepository { get; }
        ISquirrelRepository<Room> RoomRepository { get; }

        void Commit();
    }
}
