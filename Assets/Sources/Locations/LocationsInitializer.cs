using Core;

namespace Locations {
    public class LocationsInitializer : MonoInitilizable {
        public override void Initialize() {
            var location = GetProperty();
            var controller = new LocationsController(location);
            GameApplication.AddController(controller);
            IsInitialized = true;
        }

        private IPlayerProperty<string> GetProperty() {
            return GameApplication.PlayerData.GetOrCreate<string>(LocationsController.Location);
        }
    }
}
