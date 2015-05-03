using System;
using Squirrel.Services.Events;

namespace Squirrel.Services.Abstracts
{
    public interface ISquirrelServices
    {
        event EventHandler<SquirrelCommandRecognizedEventArgs> SquirrelCommandRecognized; 
        event EventHandler<SquirrelCommandRejectedEventArgs> SquirrelCommandRejected;

        int ElmaTimer { get; }

        void DeviceCommand(string command);
    }
}
