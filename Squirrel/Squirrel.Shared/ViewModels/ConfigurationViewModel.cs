using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using Squirrel.Config.Abstracts;
using Squirrel.Config.Dtos;

namespace Squirrel.ViewModels
{
    public class ConfigurationViewModel : SquirrelViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISquirrelConfiguration _squirrelConfiguration;

        private ObservableCollection<SquirrelDevice> _squirrelDevices;
        private SquirrelDevice _currentSquirrelDevice;

        public ConfigurationViewModel(INavigationService navigationService,
            ISquirrelConfiguration squirrelConfiguration)
        {
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
            this._navigationService.NavigateTo("MainPage");
        }
        #endregion
    }
}
