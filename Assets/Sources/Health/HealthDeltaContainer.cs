using Core;
using UnityEngine;

namespace Health {
    public class HealthDeltaContainer : PropertyDeltaContainer {
        [SerializeField] private HealthDelta _value = new HealthDelta(ChangingActionType.Add, 100);

        public HealthDelta Value { get => _value; }

        public override PropertyDelta GetDelta() {
            return _value;
        }
    }
}
