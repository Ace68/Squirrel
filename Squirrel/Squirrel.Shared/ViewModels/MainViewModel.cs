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
            this.ElmaCommand = new RelayCommand(this.OnElmaCommand, CanNavigateOnElmaTools);
            this.ConfigurazioneCommand = new RelayCommand(this.OnConfigurazioneCommand, CanNavigateOnConfigurazioneTools);
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
            this._navigationService.NavigateTo("CameraPage");
        }

        private static bool CanNavigateOnElmaTools()
        {
            return true;
        }

        private void OnElmaCommand()
        {
            this._navigationService.NavigateTo("ElmaPage");
        }

        private static bool CanNavigateOnConfigurazioneTools()
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
