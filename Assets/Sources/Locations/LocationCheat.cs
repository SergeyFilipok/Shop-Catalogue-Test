using Core;

namespace Locations {
    public class LocationCheat {
        private readonly LocationsController _controller;
        private readonly LocationDelta _delta;

        public LocationCheat(LocationsController controller, string defaultLocation) {
            _controller = controller;
            _delta = new LocationDelta(ChangingActionType.Set, defaultLocation);
        }

        public void Use() {
            _controller.Change(_delta);
        }
    }
}
