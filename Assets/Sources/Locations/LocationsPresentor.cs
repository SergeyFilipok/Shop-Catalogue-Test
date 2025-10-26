using Core;
using UnityEngine;

namespace Locations {
    public class LocationsPresentor : MonoBehaviour {
        [SerializeField] private CurrentLocationView _counter = null;
        [SerializeField] private string _defaultLocation = "Base";

        private LocationsController _controller;
        private LocationCheat _cheat;

        private void Start() {
            _controller = GameApplication.GetController(LocationsController.Location) as LocationsController;
            _counter.Display(_controller.Property.Value);
            _controller.Property.Changed += OnGoldChanged;

            _cheat = new LocationCheat(_controller, _defaultLocation);
            _counter.PlusClicked += OnPlusClicked;
        }

        private void OnDestroy() {
            _controller.Property.Changed -= OnGoldChanged;
        }

        private void OnGoldChanged(IReadOnlyPlayerProperty<string> property) {
            _counter.Display(property.Value);
        }

        private void OnPlusClicked() {
            _cheat.Use();
        }
    }
}
