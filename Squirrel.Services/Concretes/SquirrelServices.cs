using System;
using Squirrel.Config.Abstracts;
using Squirrel.Config.Dtos;
using Squirrel.Protocols;
using Squirrel.Services.Abstracts;
using Squirrel.Services.Commands;
using Squirrel.Services.Events;

namespace Squirrel.Services.Concretes
{
    public class SquirrelServices : ISquirrelServices, IDisposable
    {
        public event EventHandler<SquirrelCommandRecognizedEventArgs> SquirrelCommandRecognized; 
        public event EventHandler<SquirrelCommandRejectedEventArgs> SquirrelCommandRejected;

        private readonly ICommandHandler _commandHandler;
        private readonly ISquirrelConfiguration _squirrelConfiguration;

        private SquirrelDevice _currentDevice;

        public SquirrelServices(ICommandHandler commandHandler,
            ISquirrelConfiguration squirrelConfiguration)
        {
            this._commandHandler = commandHandler;
            this._squirrelConfiguration = squirrelConfiguration;
        }

        public int ElmaTimer
        {
            get { return this._squirrelConfiguration.GetElmaTimer(); }
        }

        public void DeviceCommand(string command)
        {
            this._currentDevice = this._squirrelConfiguration.GetSquirrelDeviceByName(command);

            if (this._currentDevice == null)
            {
                this.OnUnknownCOmmand();
                return;
            }

            switch (command)
            {
                case SquirrelCommands.ElmaDown:
                    this.OnElmaDownCommand();
                    break;

                case SquirrelCommands.ElmaStop:
                    this.OnElmaStopCommand();
                    break;
                    
                case SquirrelCommands.ElmaUp:
                    this.OnElmaUpCommand();
                    break;

                default:
                    this.OnUnknownCOmmand();
                    break;
            }
        }

        private void OnElmaDownCommand()
        {
            this._commandHandler.TurnOn(this._currentDevice.DeviceNumber, this._currentDevice.DeviceIstance);

            var handler = this.SquirrelCommandRecognized;
            if (handler != null) handler(this, new SquirrelCommandRecognizedEventArgs(SquirrelCommands.ElmaDown));
        }

        private void OnElmaStopCommand()
        {
            this._commandHandler.TurnOff(this._currentDevice.DeviceNumber, 1);
            this._commandHandler.TurnOff(this._currentDevice.DeviceNumber, 2);

            var handler = this.SquirrelCommandRecognized;
            if (handler != null) handler(this, new SquirrelCommandRecognizedEventArgs(SquirrelCommands.ElmaStop));
        }

        private void OnElmaUpCommand()
        {
            this._commandHandler.TurnOn(this._currentDevice.DeviceNumber, this._currentDevice.DeviceIstance);

            var handler = this.SquirrelCommandRecognized;
            if (handler != null) handler(this, new SquirrelCommandRecognizedEventArgs(SquirrelCommands.ElmaUp));
        }

        private void OnUnknownCOmmand()
        {
            var handler = this.SquirrelCommandRejected;
            if (handler != null) handler(this, new SquirrelCommandRejectedEventArgs());
        }

        #region Dispose
        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                    
            }
            this._disposed = true;
        }

        ~SquirrelServices()
        {
            Dispose(false);
        }
        #endregion
    }
}
