namespace Squirrel.Domain.Repositories
{
    public interface ISquirrelUnitofWork
    {
        //ISquirrelRepository<Device> DeviceRepository { get; }
        //ISquirrelRepository<Room> RoomRepository { get; }

        IDeviceRepository DeviceRepository { get; }
        IRoomRepository RoomRepository { get; }

        void Commit();
    }
}
