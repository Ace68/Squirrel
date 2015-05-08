using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

using Squirrel.Config.Abstracts;
using Squirrel.Config.Dtos;
using Squirrel.Config.Events;

namespace Squirrel.Config.Concretes
{
    public class SquirrelConfiguration : ISquirrelConfiguration
    {
        private const string SquirrelContainerName = "SquirrelContainer";
        private ApplicationDataContainer _squirrelSetting;
        private List<SquirrelDevice> _squirrelDevices; 

        public event EventHandler<SquirrelConfigurationSavedEventArgs> SquirrelConfigurationSaved;

        public SquirrelConfiguration()
        {
            this.CreateLocalSetting();
        }

        public string GetZWaveAddress()
        {
            try
            {
                return this._squirrelSetting.Containers[SquirrelContainerName].Values["ZWaveServerAddress"].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public int GetElmaTimer()
        {
            try
            {
                return
                    int.Parse(
                        this._squirrelSetting.Containers[SquirrelContainerName].Values["ElmaTimer"].ToString());
            }
            catch
            {
                return 1;
            }
        }

        public IList<SquirrelDevice> GetSquirrelDevice()
        {
            if (this._squirrelDevices != null) return this._squirrelDevices;

            this._squirrelDevices = new List<SquirrelDevice>
            {
                this.GetSquirrelDeviceFromContainer("LuceCamera"),
                this.GetSquirrelDeviceFromContainer("LuceStudio"),
                this.GetSquirrelDeviceFromContainer("Radio"),
                this.GetSquirrelDeviceFromContainer("ElmaUp"),
                this.GetSquirrelDeviceFromContainer("ElmaDown"),
                this.GetSquirrelDeviceFromContainer("ElmaStop")
            };

            return this._squirrelDevices;
        }

        public SquirrelDevice GetSquirrelDeviceByNumber(int deviceNumber)
        {
            this.GetSquirrelDevice();

            return this._squirrelDevices.FirstOrDefault(d => d.DeviceNumber == deviceNumber);
        }

        public SquirrelDevice GetSquirrelDeviceByName(string deviceName)
        {
            this.GetSquirrelDevice();

            return this._squirrelDevices.FirstOrDefault(d => d.DeviceName == deviceName);
        }

        public void SaveSquirrelConfiguration()
        {
            if (this._squirrelDevices == null) return;

            this.ChkAndCreateContainer();

            foreach (var device in this._squirrelDevices)
            {
                if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey(device.DeviceName))
                    this._squirrelSetting.Containers[SquirrelContainerName].Values[device.DeviceName] =
                        string.Format("{0},{1}", device.DeviceNumber, device.DeviceIstance);
                else
                {
                    this._squirrelSetting.Containers[SquirrelContainerName].Values[device.DeviceName] =
                        string.Format("{0},{1}", device.DeviceNumber, device.DeviceIstance);
                }
            }
        }

        #region Helpers
        private void CreateLocalSetting()
        {
            this.ChkAndCreateContainer();

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("ZWaveServerAddress"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["ZWaveServerAddress"] =
                    "http://192.168.99.1:8083/ZWaveAPI/Run/";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("ElmaTimer"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["ElmaTimer"] = "30";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("Radio"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["Radio"] = "2,0";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("LuceCamera"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["LuceCamera"] = "3,0";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("ElmaUp"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["ElmaUp"] = "4,1";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("ElmaDown"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["ElmaDown"] = "4,2";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("ElmaStop"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["ElmaStop"] = "4,1";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("LuceStudio"))
                this._squirrelSetting.Containers[SquirrelContainerName].Values["LuceStudio"] = "5,0";

            if (!this._squirrelSetting.Containers[SquirrelContainerName].Values.ContainsKey("Rooms"))
            {
                
            }
        }

        private void ChkAndCreateContainer()
        {
            this._squirrelSetting = ApplicationData.Current.LocalSettings;

            if (!this._squirrelSetting.Containers.ContainsKey(SquirrelContainerName))
            {
                this._squirrelSetting.CreateContainer(SquirrelContainerName, ApplicationDataCreateDisposition.Always);
            }
        }

        private SquirrelDevice GetSquirrelDeviceFromContainer(string deviceName)
        {
            try
            {
                var deviceParameters = this._squirrelSetting.Containers[SquirrelContainerName].Values[deviceName].ToString();
                var arrayParameters = deviceParameters.Split(',');

                return new SquirrelDevice
                {
                    DeviceName = deviceName,
                    DeviceNumber = int.Parse(arrayParameters.GetValue(0).ToString()),
                    DeviceIstance = int.Parse(arrayParameters.GetValue(1).ToString()),
                    DeviceType = string.Empty
                };
            }
            catch
            {
                return null;
            }
        }

        private void CreateRoomsJson()
        {
            
        }
        #endregion
    }
}
