using Squirrel.Common;
using Squirrel.Domain.Repositories;
using Squirrel.Domain.Resources.Resources;
using Squirrel.Persistence.Facade;

namespace Squirrel.Persistence.Repositories
{
    public class SquirrelUnitOfWork : ISquirrelUnitofWork
    {
        private readonly DomainModelFacade _domanModelFacade;

        public SquirrelUnitOfWork()
        {
            this._domanModelFacade = new DomainModelFacade();
        }

        private IDeviceRepository _deviceRepository;
        public IDeviceRepository DeviceRepository
        {
            get
            {
                return this._deviceRepository ??
                       (this._deviceRepository =
                           new DeviceRepository(this._domanModelFacade));
            }
        }

        private IRoomRepository _roomRepository;
        public IRoomRepository RoomRepository
        {
            get
            {
                return this._roomRepository ??
                       (this._roomRepository =
                           new RoomRepository(this._domanModelFacade));
            }
        }

        public void Commit()
        {
            #region Rooms
            var jsonRooms = this._roomRepository.GetAll(0, 0).ToJSON();
            if (
                !this._domanModelFacade.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values
                    .ContainsKey("Rooms"))
                this._domanModelFacade.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values[
                    "Rooms"] = jsonRooms;
            else
            {
                this._domanModelFacade.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values[
                    "Rooms"] = jsonRooms;
            }
            #endregion

            #region Devices
            var jsonDevices = this._deviceRepository.GetAll(0, 0).ToJSON();
            if (
                !this._domanModelFacade.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values
                    .ContainsKey("Devices"))
                this._domanModelFacade.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values[
                    "Devices"] = jsonDevices;
            else
            {
                this._domanModelFacade.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values[
                    "Devices"] = jsonDevices;
            }
            #endregion
        }
    }
}
