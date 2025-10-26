using Core;
using UnityEngine;

namespace Health {
    public class HealthCheat : MonoBehaviour {
        private readonly HealthController _controller;
        private readonly HealthDelta _delta;

        public HealthCheat(HealthController controller, int value) {
            _controller = controller;
            _delta = new HealthDelta(ChangingActionType.Add, value);
        }

        public void Use() {
            _controller.Change(_delta);
        }
    }
}
