using System;
using System.Net;

using Windows.Storage;

using Squirrel.Protocols;

namespace Squirrel.Protocls.ZWave
{
    public class CommandHandler : CommandHandlerBase
    {
        private string _zwaveServerAddress;

        public CommandHandler()
        {
            this.ReadSquirrelConfiguration();
        }

        public override void TurnOff(int deviceId, int instanceId)
        {
            this.SendCommand(deviceId, instanceId, 0);
        }

        public override void TurnOn(int deviceId, int instanceId)
        {
            this.SendCommand(deviceId, instanceId, 255);
        }

        #region Private Methods
        private void SendCommand(int deviceId, int instanceId, int value)
        {
            var deviceUrl = string.Format("{0}/devices[{1}].instances[{2}].commandClasses[0x25].Set({3})", this._zwaveServerAddress, deviceId, instanceId, value);

            try
            {
                var webRequest = WebRequest.Create(deviceUrl);
                webRequest.Method = "POST";
                webRequest.GetResponseAsync().ContinueWith(x => x.Result.Dispose());
            }
            catch (Exception)
            {
                
                
            }
        }
        #endregion

        private void ReadSquirrelConfiguration()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            if (!localSettings.Containers.ContainsKey("SquirrelContainer")) return;

            if (localSettings.Containers["SquirrelContainer"].Values.ContainsKey("ZWaveServerAddress"))
                this._zwaveServerAddress = localSettings.Containers["SquirrelContainer"].Values["ZWaveServerAddress"].ToString();
        }
    }
}
