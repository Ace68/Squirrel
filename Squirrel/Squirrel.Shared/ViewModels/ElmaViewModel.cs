using System;
using Windows.UI.Xaml;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using Squirrel.Services.Abstracts;
using Squirrel.Services.Commands;
using Squirrel.Services.Events;

namespace Squirrel.ViewModels
{
    public class ElmaViewModel : SquirrelViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISquirrelServices _squirrelServices;

        private readonly DispatcherTimer _squirrelTimer = new DispatcherTimer();

        private string _commandStatus = "Waiting...";

        public ElmaViewModel(INavigationService navigationService,
            ISquirrelServices squirrelServices)
        {
            this._navigationService = navigationService;
            this._squirrelServices = squirrelServices;

            this.GoBackCommand = new RelayCommand(this.OnGoBackCommand);
            this.ElmaUpCommand = new RelayCommand(this.OnElmaUpCommand);
            this.ElmaStopCommand = new RelayCommand(this.OnElmaStopCommand);
            this.ElmaDownCommand = new RelayCommand(this.OnElmaDownCommand);

            this._squirrelServices.SquirrelCommandRecognized += _squirrelServices_SquirrelCommandRecognized;
            this._squirrelServices.SquirrelCommandRejected += _squirrelServices_SquirrelCommandRejected;

            this.ConfigureSquirrelTimer();
        }

        #region Public Properties
        public string CommandStatus
        {
            get { return this._commandStatus; }
            set
            {
                if (this._commandStatus == value)return;

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
                case SquirrelCommands.ElmaDown:
                    this.CommandStatus = "Elma is Going Down";
                    break;

                case SquirrelCommands.ElmaStop:
                    this.CommandStatus = "Elma Stop";
                    break;

                case SquirrelCommands.ElmaUp:
                    this.CommandStatus = "Elma is Going Up";
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
        public RelayCommand ElmaUpCommand { get; private set; }
        public RelayCommand ElmaStopCommand { get; private set; }
        public RelayCommand ElmaDownCommand { get; private set; }
        
        private void OnGoBackCommand()
        {
            this._navigationService.NavigateTo("MainPage");
        }

        private void OnElmaDownCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.ElmaDown);
            this._squirrelTimer.Start();
        }

        private void OnElmaStopCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.ElmaStop);
        }

        private void OnElmaUpCommand()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.ElmaUp);
            this._squirrelTimer.Start();
        }
        #endregion

        #region Helpers
        private void ConfigureSquirrelTimer()
        {
            this._squirrelTimer.Interval = new TimeSpan(0, 0, this._squirrelServices.ElmaTimer);
            this._squirrelTimer.Tick += _squirrelTimer_Tick;
        }

        private void _squirrelTimer_Tick(object sender, object e)
        {
            this._squirrelTimer.Stop();
            this.OnSquirrelTimerTick();
        }

        private void OnSquirrelTimerTick()
        {
            this._squirrelServices.DeviceCommand(SquirrelCommands.ElmaStop);
        }
        #endregion
    }
}
