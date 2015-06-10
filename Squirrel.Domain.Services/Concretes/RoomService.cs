using System;
using System.Collections.Generic;
using System.Linq;
using Squirrel.Domain.Entities;
using Squirrel.Domain.Repositories;
using Squirrel.Domain.Resources.Exceptions;
using Squirrel.Domain.Resources.Resources;
using Squirrel.Domain.Services.Abstracts;
using Squirrel.Domain.Services.Dtos;

namespace Squirrel.Domain.Services.Concretes
{
    public class RoomService : IRoomService
    {
        private readonly ISquirrelUnitofWork _squirrelUnitofWork;

        public RoomService(ISquirrelUnitofWork squirrelUnitofWork)
        {
            this._squirrelUnitofWork = squirrelUnitofWork;
        }

        public List<DtoRoom> GetRoomsFromRepository()
        {
            var roomsList = this._squirrelUnitofWork.RoomRepository.GetAll(0, 0);

            var dtoRoomsList = new List<DtoRoom>();
            
            #region Create Rooms
            if (!roomsList.Any())
            {
                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.HallName
                });

                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.KitchenName
                });

                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.LivingRoomName
                });

                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.OfficeName
                });

                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.BedRoomName
                });

                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.BathRoom
                });

                dtoRoomsList.Add(new DtoRoom
                {
                    Id = Guid.NewGuid(),
                    RoomName = SquirrelResources.Garden
                });

                return dtoRoomsList;
            }
            #endregion

            return dtoRoomsList;
        }

        public void SaveRoomsIntoRepository(IList<DtoRoom> roomsToSave)
        {
            if (roomsToSave == null)
                throw new ArgumentNullException("roomsToSave", DomainException.RoomsListNullException);

            foreach (var room in roomsToSave)
            {
                var currentRoom = Room.CreateRoom(room.Id, room.RoomName);
                this._squirrelUnitofWork.RoomRepository.Insert(currentRoom);
            }

            this._squirrelUnitofWork.Commit();
        }
    }
}
