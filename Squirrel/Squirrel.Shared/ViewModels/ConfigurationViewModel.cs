using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using Squirrel.Config.Abstracts;
using Squirrel.Config.Dtos;
using Squirrel.Services.Abstracts;

namespace Squirrel.ViewModels
{
    public class ConfigurationViewModel : SquirrelViewModelBase
    {
        private readonly IAlertMessageService _alertMessageService;
        private readonly INavigationService _navigationService;
        private readonly ISquirrelConfiguration _squirrelConfiguration;

        private ObservableCollection<SquirrelDevice> _squirrelDevices;
        private SquirrelDevice _currentSquirrelDevice;

        public ConfigurationViewModel(IAlertMessageService alertMessageService,
            INavigationService navigationService,
            ISquirrelConfiguration squirrelConfiguration)
        {
            this._alertMessageService = alertMessageService;
            this._navigationService = navigationService;
            this._squirrelConfiguration = squirrelConfiguration;

            this.GoBackCommand = new RelayCommand(this.OnGoBackCommand);

            this.LoadSquirrelDevices();
        }

        #region Public Properties
        public ObservableCollection<SquirrelDevice> SquirrelDevices
        {
            get { return this._squirrelDevices; }
            set
            {
                if (this._squirrelDevices == value) return;

                this._squirrelDevices = value;
                this.NotifyPropertyChanged("SquirrelDevices");
            }
        }

        public SquirrelDevice CurrentSquirrelDevice
        {
            get { return this._currentSquirrelDevice; }
            set
            {
                if (this._currentSquirrelDevice == value) return;

                this._currentSquirrelDevice = value;
                this.NotifyPropertyChanged("CurrentSquirrelDevice");
            }
        }
        #endregion

        #region ViewModel Method
        private void LoadSquirrelDevices()
        {
            var elencoDevices = this._squirrelConfiguration.GetSquirrelDevice();
            
            if (this.SquirrelDevices == null)
                this.SquirrelDevices = new ObservableCollection<SquirrelDevice>();

            this.SquirrelDevices.Clear();

            foreach (var device in elencoDevices)
            {
                this.SquirrelDevices.Add(device);
            }
        }
        #endregion

        #region Command
        public RelayCommand GoBackCommand { get; private set; }

        private void OnGoBackCommand()
        {
            this.SalvaConfigurazioneRequest();
        }
        #endregion

        private void SalvaConfigurazioneRequest()
        {
            Action confirmSaveConfigurationAction = this.ConfermaSalvaConfigurazione;
            Action cancelSaveConfigurationAction = AnnullaSalvaConfigurazione;

            var commandList = new List<DialogCommand>
            {
                new DialogCommand
                {
                    Id = 1,
                    Label = "Conferma",
                    Invoked = confirmSaveConfigurationAction
                },
                new DialogCommand
                {
                    Id = 2,
                    Label = "Annulla",
                    Invoked = cancelSaveConfigurationAction
                }
            };

            this._alertMessageService.ShowAsync("Confermi Salvataggio della Configurazione?", "Squirrel Configuration",
                commandList);
        }

        private void ConfermaSalvaConfigurazione()
        {
            this._squirrelConfiguration.SaveSquirrelConfiguration();
            this._navigationService.NavigateTo("MainPage");
        }

        private void AnnullaSalvaConfigurazione()
        {
            this._navigationService.NavigateTo("MainPage");
        }
    }
}
