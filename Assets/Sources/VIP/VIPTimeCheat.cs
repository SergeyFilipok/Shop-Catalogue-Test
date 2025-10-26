using System;

namespace VIP {
    public class VIPTimeCheat {
        private readonly VIPController _controller;
        private readonly TimeDelta _delta;

        public VIPTimeCheat(VIPController controller, TimeSpan valueToAdd) {
            _controller = controller;
            _delta = new TimeDelta(valueToAdd, Core.ChangingActionType.Add);
        }

        public void Use() {
            _controller.Change(_delta);
        }
    }
}
