using Core;
using UnityEngine;

namespace Gold {
    public class GoldDeltaContainer : PropertyDeltaContainer {
        [SerializeField] private GoldDelta _value = new GoldDelta(ChangingActionType.Add, 100);

        public GoldDelta Value { get => _value; }

        public override PropertyDelta GetDelta() {
            return _value;
        }
    }
}
