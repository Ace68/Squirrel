using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

using Squirrel.Config.Abstracts;
using Squirrel.Config.Dtos;
using Squirrel.Config.Events;
using Squirrel.Domain.Resources.Resources;
using Squirrel.Domain.Services.Abstracts;
using Squirrel.Domain.Services.Dtos;

namespace Squirrel.Config.Concretes
{
    public class SquirrelConfiguration : ISquirrelConfiguration
    {
        private ApplicationDataContainer _squirrelSetting;
        private List<SquirrelDevice> _squirrelDevices;

        private List<DtoRoom> _rooms;

        private readonly IRoomService _roomService;

        public event EventHandler<SquirrelConfigurationSavedEventArgs> SquirrelConfigurationSaved;

        public SquirrelConfiguration(IRoomService roomService)
        {
            this._roomService = roomService;

            this.CreateLocalSetting();
        }

        public string GetZWaveAddress()
        {
            try
            {
                return this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ZWaveServerAddress"].ToString();
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
                        this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ElmaTimer"].ToString());
            }
            catch
            {
                return 1;
            }
        }

        public IList<SquirrelDevice> GetSquirrelDevice()
        {
            this._rooms = this._roomService.GetRoomsFromRepository();

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
            this._roomService.SaveRoomsIntoRepository(this._rooms);

            if (this._squirrelDevices == null) return;

            this.ChkAndCreateContainer();

            foreach (var device in this._squirrelDevices)
            {
                if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey(device.DeviceName))
                    this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values[device.DeviceName] =
                        string.Format("{0},{1}", device.DeviceNumber, device.DeviceIstance);
                else
                {
                    this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values[device.DeviceName] =
                        string.Format("{0},{1}", device.DeviceNumber, device.DeviceIstance);
                }
            }
        }

        #region Helpers
        private void CreateLocalSetting()
        {
            this.ChkAndCreateContainer();

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("ZWaveServerAddress"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ZWaveServerAddress"] =
                    "http://192.168.99.1:8083/ZWaveAPI/Run/";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("ElmaTimer"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ElmaTimer"] = "30";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("Radio"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["Radio"] = "2,0";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("LuceCamera"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["LuceCamera"] = "3,0";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("ElmaUp"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ElmaUp"] = "4,1";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("ElmaDown"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ElmaDown"] = "4,2";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("ElmaStop"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["ElmaStop"] = "4,1";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("LuceStudio"))
                this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values["LuceStudio"] = "5,0";

            if (!this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values.ContainsKey("Rooms"))
            {
                
            }
        }

        private void ChkAndCreateContainer()
        {
            this._squirrelSetting = ApplicationData.Current.LocalSettings;

            if (!this._squirrelSetting.Containers.ContainsKey(SquirrelResources.SquirrelContainerName))
            {
                this._squirrelSetting.CreateContainer(SquirrelResources.SquirrelContainerName, ApplicationDataCreateDisposition.Always);
            }
        }

        private SquirrelDevice GetSquirrelDeviceFromContainer(string deviceName)
        {
            try
            {
                var deviceParameters = this._squirrelSetting.Containers[SquirrelResources.SquirrelContainerName].Values[deviceName].ToString();
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
        #endregion
    }
}
