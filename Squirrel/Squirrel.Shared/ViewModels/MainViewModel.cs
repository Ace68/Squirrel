using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace Squirrel.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private Visibility _mostraIngresso = Visibility.Collapsed;
        private Visibility _mostraSoggiorno = Visibility.Collapsed;
        private Visibility _mostraStudio = Visibility.Collapsed;
        private Visibility _mostraCamera = Visibility.Collapsed;
        private Visibility _mostraBagno = Visibility.Collapsed;
        private Visibility _mostraCucina = Visibility.Collapsed;
        private Visibility _mostraEsterno = Visibility.Collapsed;

        public MainViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            this.IngressoCommand = new RelayCommand(this.OnIngressoCommand, this.CanNavigateOnIngressoTools);
            this.CameraCommand = new RelayCommand(this.OnCameraCommand, CanNavigateOnCameraTools);
            this.ElmaCommand = new RelayCommand(this.OnElmaCommand, CanNavigateOnElmaTools);
            this.ConfigurazioneCommand = new RelayCommand(this.OnConfigurazioneCommand, CanNavigateOnConfigurazioneTools);

            this.ChkDevicesForRoomsVisibility();
        }

        #region Public Properties
        public Visibility MostraIngresso
        {
            get { return this._mostraIngresso; }
        }

        public Visibility MostraSoggiorno
        {
            get { return this._mostraSoggiorno; }
        }

        public Visibility MostraStudio
        {
            get { return this._mostraStudio; }
        }

        public Visibility MostraCamera
        {
            get { return this._mostraCamera; } 
        }

        public Visibility MostraBagno
        {
            get { return this._mostraBagno; }
        }

        public Visibility MostraCucina
        {
            get { return this._mostraCucina; }
        }

        public Visibility MostraEsterno
        {
            get { return this._mostraEsterno; }
        }
        #endregion

        #region Command
        public RelayCommand IngressoCommand { get; private set; }
        public RelayCommand CameraCommand { get; private set; }
        public RelayCommand ElmaCommand { get; private set; }
        public RelayCommand ConfigurazioneCommand { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// TODO: La visibilità dipenderà dal controllo dei Devices.
        /// Se ad una stanza sono assegnati Devices => Visible
        /// Altrimenti => Collapsed
        /// </summary>
        private void ChkDevicesForRoomsVisibility()
        {
            this._mostraIngresso = Visibility.Collapsed;
            this._mostraSoggiorno = Visibility.Collapsed;
            this._mostraStudio = Visibility.Collapsed;
            this._mostraCamera = Visibility.Visible;
            this._mostraBagno = Visibility.Collapsed;
            this._mostraCucina = Visibility.Collapsed;
            this._mostraEsterno = Visibility.Visible;
        }

        private bool CanNavigateOnIngressoTools()
        {
            return this._mostraIngresso == Visibility.Visible;
        }

        private void OnIngressoCommand()
        {
            this._navigationService.NavigateTo("IngressoPage");
        }

        private bool CanNavigateOnCameraTools()
        {
            return this._mostraCamera == Visibility.Visible;
        }

        private void OnCameraCommand()
        {
            this._navigationService.NavigateTo("CameraPage");
        }

        private bool CanNavigateOnElmaTools()
        {
            return this._mostraEsterno == Visibility.Visible;
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
