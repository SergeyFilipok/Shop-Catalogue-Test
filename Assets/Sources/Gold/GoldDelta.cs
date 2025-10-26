using Core;
using System;
using UnityEngine;

namespace Gold {
    [Serializable]
    public class GoldDelta : PropertyDelta<int> {
        [SerializeField] private ChangingActionType _action = default;
        [SerializeField] private int _value = 0;

        public GoldDelta(ChangingActionType actionType, int value) {
            _action = actionType;
            _value = value;
        }

        public override string TargetID => GoldController.Gold;
        public override ChangingActionType ActionType => _action;
        public override int Value => _value;
    }
}
