using Core;
using UnityEngine;

namespace Health {
    public class HealthDelta : PropertyDelta<int> {
        [SerializeField] private ChangingActionType _action = default;
        [SerializeField] private int _value = 0;

        public HealthDelta(ChangingActionType actionType, int value) {
            _action = actionType;
            _value = value;
        }

        public override string TargetID => HealthController.Health;
        public override ChangingActionType ActionType => _action;
        public override int Value => _value;
    }
}
