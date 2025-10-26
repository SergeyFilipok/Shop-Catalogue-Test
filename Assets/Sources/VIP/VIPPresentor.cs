using Core;
using System;
using UnityEngine;

namespace VIP {
    public class VIPPresentor : MonoBehaviour {
        [SerializeField] private VIPTimerView _counter = null;
        [SerializeField] private int _plusSeconds = 30;

        private VIPController _controller;
        private VIPTimeCheat _cheat;

        private void Awake() {
            _controller = GameApplication.GetController(VIPController.VIP) as VIPController;
            _counter.Display(_controller.Property.Value);
            _controller.Property.Changed += OnPropertyChanged;

            _cheat = new VIPTimeCheat(_controller, TimeSpan.FromSeconds(_plusSeconds));
            _counter.PlusClicked += OnPlusClicked;
        }

        private void OnPlusClicked() {
            _cheat.Use();
        }

        private void OnPropertyChanged(IReadOnlyPlayerProperty<TimeSpan> property) {
            _counter.Display(property.Value);
        }
    }
}
