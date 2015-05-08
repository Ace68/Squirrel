using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using Microsoft.Practices.ServiceLocation;
using Squirrel.Config.Abstracts;
using Squirrel.Config.Concretes;
using Squirrel.Protocols.ZWave;
using Squirrel.Protocols;
using Squirrel.Services.Abstracts;
using Squirrel.Services.Concretes;

namespace Squirrel.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public RoomsViewModel RoomsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<RoomsViewModel>(); }
        }

        public ConfigurationViewModel ConfigurationViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ConfigurationViewModel>(); }
        }

        static ViewModelLocator()
        {
            // Mi ispira poco il fatto che Unity sia in pre-release per WindowsPhone 8.1
            // Meglio attendere il rilascio ufficiale.
            // Bisogna includere nel progetto la classe UnitySeviceLocator nella cartella ViewModels
            //var container = new UnityContainer();
            //ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            //container.RegisterType<ISquirrelServices, SquirrelServices>(new HierarchicalLifetimeManager());

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ISquirrelServices, SquirrelServices>();
            SimpleIoc.Default.Register<ICommandHandler, CommandHandler>();
            SimpleIoc.Default.Register<ISquirrelConfiguration, SquirrelConfiguration>();
            SimpleIoc.Default.Register<IAlertMessageService, AlertMessageService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RoomsViewModel>();
            SimpleIoc.Default.Register<ConfigurationViewModel>();
            
            //SimpleIoc.Default.Register<INavigationService, NavigationService>();
            var navigationService = CreateNavigationService();
            SimpleIoc.Default.Register(() => navigationService);
        }

        /// <summary>
        /// Configurazione del servizio di navigazione
        /// </summary>
        /// <returns></returns>
        static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("MainPage", typeof(MainPage));
            navigationService.Configure("CameraPage", typeof(CameraPage));
            navigationService.Configure("ElmaPage", typeof(ElmaPage));
            navigationService.Configure("ConfigurationPage", typeof(ConfigurationPage));

            return navigationService;
        }
    }
}
