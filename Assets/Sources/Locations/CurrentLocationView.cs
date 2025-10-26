using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Locations {
    public class CurrentLocationView : MonoBehaviour {
        [SerializeField] private Button _plusButton = null;
        [SerializeField] private TMP_Text _valueText = null;

        public event Action PlusClicked;

        private void Awake() {
            _plusButton.onClick.AddListener(() => PlusClicked?.Invoke());
        }

        public void Display(string value) {
            _valueText.text = value;
        }

        private void OnDestroy() {
            PlusClicked = null;
        }
    }
}
