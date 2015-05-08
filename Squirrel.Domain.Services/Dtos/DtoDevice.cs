using System;

namespace Squirrel.Domain.Services.Dtos
{
    public class DtoDevice : DtoBase
    {
        public int DeviceNumber { get; set; }
        public int DeviceIstance { get; set; }
        public Guid DeviceRoomId { get; set; }
    }
}
