using System.Collections.ObjectModel;

using Windows.Storage;
using Windows.UI.StartScreen;
using Newtonsoft.Json;
using Squirrel.Domain.Entities;
using Squirrel.Domain.Resources.Resources;

namespace Squirrel.Persistence.Facade
{
    public class DomainModelFacade
    {
        public ApplicationDataContainer SquirrelContainer;

        public ObservableCollection<Device> Device { get; set; }
        public ObservableCollection<Room> Room { get; set; }

        public DomainModelFacade()
        {
            this.ChkAndCreateContainer();

            this.Device = new ObservableCollection<Device>();
            this.GetRoomsCollectionFromContainer();
        }

        #region Helpers
        /// <summary>
        /// Verify and/or Create a LocalSettings Container
        /// </summary>
        private void ChkAndCreateContainer()
        {
            this.SquirrelContainer = ApplicationData.Current.LocalSettings;

            if (!this.SquirrelContainer.Containers.ContainsKey(SquirrelResources.SquirrelContainerName))
            {
                this.SquirrelContainer.CreateContainer(SquirrelResources.SquirrelContainerName, ApplicationDataCreateDisposition.Always);
            }
        }

        /// <summary>
        /// Verify if in Device Container there is Rooms Data
        /// </summary>
        private void GetRoomsCollectionFromContainer()
        {
            try
            {
                var rooms =
                    this.SquirrelContainer.Containers[SquirrelResources.SquirrelContainerName].Values["Rooms"].ToString();

                var jsonRooms = string.Concat("{'rooms':", rooms, "}");
                this.Room = JsonConvert.DeserializeObject<ObservableCollection<Room>>(jsonRooms);
            }
            catch
            {
                this.Room = new ObservableCollection<Room>();
            }
        }
        #endregion
    }
}
