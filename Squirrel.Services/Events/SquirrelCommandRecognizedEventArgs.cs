using System;

namespace Squirrel.Services.Events
{
    public class SquirrelCommandRecognizedEventArgs : EventArgs
    {
        public string Command { get; private set; }

        public SquirrelCommandRecognizedEventArgs(string command)
        {
            this.Command = command;
        }
    }
}
