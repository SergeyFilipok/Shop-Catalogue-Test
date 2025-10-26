using Core;
using UnityEngine;

namespace Health {
    public class GoldPresentor : MonoBehaviour {
        [SerializeField] private HealthCounterView _counter = null;
        [SerializeField] private int _plusValue = 50;

        private HealthController _controller;
        private HealthCheat _cheat;

        private void Start() {
            _controller = GameApplication.GetController(HealthController.Health) as HealthController;
            _counter.Display(_controller.Property.Value);
            _controller.Property.Changed += OnGoldChanged;

            _cheat = new HealthCheat(_controller, _plusValue);
            _counter.PlusClicked += OnPlusClicked;
        }

        private void OnDestroy() {
            _controller.Property.Changed -= OnGoldChanged;
        }

        private void OnGoldChanged(IReadOnlyPlayerProperty<int> property) {
            _counter.Display(property.Value);
        }

        private void OnPlusClicked() {
            _cheat.Use();
        }
    }
}
