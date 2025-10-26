using Core;
using UnityEngine;

namespace Locations {
    public class LocationDeltaContainer : PropertyDeltaContainer {
        [SerializeField] private LocationDelta _value = new LocationDelta(ChangingActionType.Set, "none");

        public LocationDelta Value { get => _value; }

        public override PropertyDelta GetDelta() {
            return _value;
        }
    }
}
