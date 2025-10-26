using Core;
using System;
using UnityEngine;

namespace Locations {
    [Serializable]
    public class LocationDelta : PropertyDelta<string> {
        [SerializeField] private ChangingActionType _action = ChangingActionType.Set;
        [SerializeField] private string _newLocation = "None";

        public LocationDelta(ChangingActionType actionType, string value) {
            _action = actionType;
            _newLocation = value;
        }

        public override string TargetID => LocationsController.Location;
        public override ChangingActionType ActionType => _action;
        public override string Value => _newLocation;
    }
}
