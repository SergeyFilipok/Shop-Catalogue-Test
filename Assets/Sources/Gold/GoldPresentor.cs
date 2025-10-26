using Core;
using UnityEngine;

namespace Gold {
    public class GoldPresentor : MonoBehaviour {
        [SerializeField] private GoldCounterView _counter = null;
        [SerializeField] private int _plusValue = 50;

        private GoldController _controller;
        private GoldCheat _cheat;

        private void Start() {
            _controller = GameApplication.GetController(GoldController.Gold) as GoldController;
            _counter.Display(_controller.Property.Value);
            _controller.Property.Changed += OnGoldChanged;

            _cheat = new GoldCheat(_controller, _plusValue);
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
