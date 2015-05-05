using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using Squirrel.Services.Abstracts;
using Squirrel.Services.Commands;
using Squirrel.Services.Events;

namespace Squirrel.ViewModels
{
    public class CameraViewModel : SquirrelViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISquirrelServices _squirrelServices;

        private string _commandStatus = "Waiting...";

        public CameraViewModel(INavigationService navigationService,
            ISquirrelServices squirrelServices)
        {
            this._navigationService = navigationService;
            this._squirrelServices = squirrelServices;

            this.GoBackCommand = new RelayCommand(this.OnGoBackCommand);
            this.LuceCameraOffCommand = new RelayCommand(this.OnLuceCameraOffCommand);
            this.LuceCameraOnCommand = new RelayCommand(this.OnLuceCameraOnCommand);
            this.RadioOffCommand = new RelayCommand(this.OnRadioOffCommand);
            this.RadioOnCommand = new RelayCommand(this.OnRadioOnCommand);

            this._squirrelServices.SquirrelCommandRecognized += _squirrelServices_SquirrelCommandRecognized;
            this._squirrelServices.SquirrelCommandRejected += _squirrelServices_SquirrelCommandRejected;
        }

        #region Public Properties
        public string CommandStatus
        {
            get { return this._commandStatus; }
            set
            {
                if (this._commandStatus == value) return;

                this._commandStatus = value;
                this.NotifyPropertyChanged("CommandStatus");
            }
        }
        #endregion

        #region SquirrelService EventHandler
        private void _squirrelServices_SquirrelCommandRecognized(object sender, SquirrelCommandRecognizedEventArgs e)
        {
            switch (e.Command)
            {
                case SquirrelCommands.LuceCameraOff:
                    this.CommandStatus = "Luce in Going Off";
                    break;

                case SquirrelCommands.LuceCameraOn:
                    this.CommandStatus = "Luce in Going On";
                    break;

                case SquirrelCommands.RadioOff:
                    this.CommandStatus = "Radio is Going Off";
                    break;

                case SquirrelCommands.RadioOn:
                    this.CommandStatus = "Radio is Going On";
                    break;
            }
        }

        private void _squirrelServices_SquirrelCommandRejected(object sender, SquirrelCommandRejectedEventArgs e)
        {
            this.CommandStatus = "Unknown Command";
        }
        #endregion

        #region Command
        public RelayCommand GoBackCommand { get; private set; }
        public RelayCommand LuceCameraOffCommand { get; private set; }
        public RelayCommand LuceCameraOnCommand { get; private set; }
        public RelayCommand RadioOffCommand { get; private set; }
        public RelayCommand RadioOnCommand { get; private set; }
        
        private void OnGoBackCommand()
        {
            this._navigationService.NavigateTo("MainPage");
        }

        private void OnLuceCameraOffCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.LuceCameraOff);
        }

        private void OnLuceCameraOnCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.LuceCameraOn);
        }

        private void OnRadioOffCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.RadioOff);
        }

        private void OnRadioOnCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.RadioOn);
        }
        #endregion
    }
}
