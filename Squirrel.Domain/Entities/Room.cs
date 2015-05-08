using System;
using Squirrel.Domain.Abstracts;
using Squirrel.Domain.Resources.Exceptions;

namespace Squirrel.Domain.Entities
{
    public class Room : EntityBase
    {
        public string RoomName { get; private set; }

        protected Room()
        { }

        public static Room CreateRoom(Guid roomId, string roomName)
        {
            if (String.IsNullOrEmpty(roomName) || String.IsNullOrWhiteSpace(roomName))
                throw new ArgumentNullException("roomName", DomainException.RoomNameNullException);

            if (roomId == Guid.Empty)
                roomId = Guid.NewGuid();

            return new Room
            {
                Id = roomId,
                RoomName = roomName
            };
        }
    }
}
