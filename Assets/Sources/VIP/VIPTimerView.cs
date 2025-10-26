using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VIP {
    public class VIPTimerView : MonoBehaviour {
        private const string EMPTY = "NOT VIP";

        [SerializeField] private Button _plusButton = null;
        [SerializeField] private TMP_Text _valueText = null;

        public event Action PlusClicked;

        private void Awake() {
            _plusButton.onClick.AddListener(() => PlusClicked?.Invoke());
        }

        public void Display(TimeSpan value) {
            if (value > TimeSpan.Zero) {
                _valueText.text = ((int)value.TotalSeconds).ToString() + " sec";
            } else {
                _valueText.text = EMPTY;
            }
        }

        private void OnDestroy() {
            PlusClicked = null;
        }
    }
}
