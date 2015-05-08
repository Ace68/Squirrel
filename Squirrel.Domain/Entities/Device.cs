using System;

using Squirrel.Domain.Abstracts;
using Squirrel.Domain.Resources.Exceptions;

namespace Squirrel.Domain.Entities
{
    public class Device : EntityBase
    {
        public int DeviceNumber { get; private set; }
        public int DeviceIstance { get; private set; }
        public Guid DeviceRoomId { get; private set; }

        protected Device()
        { }

        public static Device CreateDevice(Guid deviceId, int deviceNumber, int deviceIstance)
        {
            if (deviceNumber <= 0)
                throw new ArgumentNullException("deviceNumber", DomainException.DeviceNumberNullException);

            if (deviceIstance <0)
                throw new ArgumentNullException("deviceIstance", DomainException.DeviceIstanceNullException);

            if (deviceId == Guid.Empty)
                deviceId = Guid.NewGuid();

            return new Device
            {
                Id = deviceId,
                DeviceNumber = deviceNumber,
                DeviceIstance = deviceIstance,
                DeviceRoomId = Guid.Empty
            };
        }

        public void SetRoomForDevice(Guid deviceId, Guid roomId)
        {
            this.DeviceRoomId = deviceId;
        }
    }
}
