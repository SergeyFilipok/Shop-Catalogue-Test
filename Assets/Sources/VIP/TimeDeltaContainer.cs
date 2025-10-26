using Core;
using UnityEngine;

namespace VIP {
    public class TimeDeltaContainer : PropertyDeltaContainer {
        [SerializeField] private TimeDelta _time = new TimeDelta();

        public TimeDelta Delta => _time;

        public override PropertyDelta GetDelta() {
            return _time;
        }
    }
}
