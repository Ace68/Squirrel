using System;
using System.Collections.Generic;
using Squirrel.Config.Dtos;
using Squirrel.Config.Events;

namespace Squirrel.Config.Abstracts
{
    public interface ISquirrelConfiguration
    {
        event EventHandler<SquirrelConfigurationSavedEventArgs> SquirrelConfigurationSaved;

        string GetZWaveAddress();
        int GetElmaTimer();
        IList<SquirrelDevice> GetSquirrelDevice();

        SquirrelDevice GetSquirrelDeviceByNumber(int deviceNumber);
        SquirrelDevice GetSquirrelDeviceByName(string deviceName);

        void SaveSquirrelConfiguration();
    }
}
