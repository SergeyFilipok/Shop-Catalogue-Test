using Core;
using System;

namespace Locations {
    public class LocationsController : IPropertyController<string> {
        public const string Location = "Location";
        public const string DEFAULT_LOCATION = "Home";

        private IPlayerProperty<string> _currentLocation;

        public IReadOnlyPlayerProperty<string> Property => _currentLocation;
        IPlayerProperty IPropertyController.Property => _currentLocation;

        public LocationsController(IPlayerProperty<string> property) {
            _currentLocation = property;
            _currentLocation.Value = DEFAULT_LOCATION;
        }

        public void Change(PropertyDelta delta) {
            if (delta is PropertyDelta<string> strDelta) {
                Change(strDelta);
            } else {
                throw new ArgumentException("Parameret delta has wrong type!");
            }
        }

        public bool CanHandle(PropertyDelta delta) {
            if (delta is PropertyDelta<string> strDelta) {
                return CanHandle(strDelta);
            } else {
                throw new ArgumentException("Parameret delta has wrong type!");
            }
        }

        //I could add check value.
        //for instance, we have list of all locations.
        //and we can check contains list the given location or not.
        public void Change(PropertyDelta<string> delta) {
            if (delta.ActionType == ChangingActionType.Set) {
                _currentLocation.Value = delta.Value;
            } else {
                throw new InvalidOperationException("Unsupported action type!");
            }
        }

        public bool CanHandle(PropertyDelta<string> delta) {
            return delta.ActionType == ChangingActionType.Set;
        }

        public void Dispose() {
            _currentLocation.Dispose();
            _currentLocation = null;
        }
    }
}
