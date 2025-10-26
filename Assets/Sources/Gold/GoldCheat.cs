using Core;

namespace Gold {
    public class GoldCheat {
        private readonly GoldController _controller;
        private readonly GoldDelta _delta;

        public GoldCheat(GoldController controller, int value) {
            _controller = controller;
            _delta = new GoldDelta(ChangingActionType.Add, value);
        }

        public void Use() {
            _controller.Change(_delta);
        }
    }
}
