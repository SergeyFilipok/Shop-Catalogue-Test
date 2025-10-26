using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gold {
    public class GoldCounterView : MonoBehaviour {
        [SerializeField] private Button _plusButton = null;
        [SerializeField] private TMP_Text _valueText = null;

        public event Action PlusClicked;

        private void Awake() {
            _plusButton.onClick.AddListener(() => PlusClicked?.Invoke());
        }

        public void Display(int value) {
            _valueText.text = value.ToString();
        }

        private void OnDestroy() {
            PlusClicked = null;
        }
    }
}
