using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace Squirrel.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            this.CameraCommand = new RelayCommand(this.OnCameraCommand, CanNavigateOnCameraTools);
            this.ElmaCommand = new RelayCommand(this.OnElmaCommand, this.CanNavigateOnElmaTools);
            this.ConfigurazioneCommand = new RelayCommand(this.OnConfigurazioneCommand, this.CanNavigateOnConfigurazioneTools);
        }

        #region Command
        public RelayCommand CameraCommand { get; private set; }
        public RelayCommand ElmaCommand { get; private set; }
        public RelayCommand ConfigurazioneCommand { get; private set; }
        #endregion

        #region Private Methods
        private static bool CanNavigateOnCameraTools()
        {
            return true;
        }

        private void OnCameraCommand()
        {
            //this._navigationService.NavigateTo("CameraPage");
        }

        private bool CanNavigateOnElmaTools()
        {
            return true;
        }

        private void OnElmaCommand()
        {
            this._navigationService.NavigateTo("ElmaPage");
        }

        private bool CanNavigateOnConfigurazioneTools()
        {
            return true;
        }

        private void OnConfigurazioneCommand()
        {
            this._navigationService.NavigateTo("ConfigurationPage");
        }
        #endregion
    }
}
