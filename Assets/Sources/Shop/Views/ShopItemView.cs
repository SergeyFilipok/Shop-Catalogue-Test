using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shopping {
    public class ShopItemView : MonoBehaviour {
        private const string BUY_TEXT = "Buy";
        private const string PROCESS_TEXT = "Processing...";

        [SerializeField] private CanvasGroup _canvasGroup = null;
        [SerializeField] private Button _infoButton = null;
        [SerializeField] private Button _buyButton = null;
        [SerializeField] private TMP_Text _buyButtonText = null;
        [SerializeField] private TMP_Text _itemName = null;

        public event Action<ShopItemView> ShowInfoClicked;
        public event Action<ShopItemView> BuyClicked;

        private void Awake() {

            _infoButton.onClick.AddListener(() => ShowInfoClicked?.Invoke(this));
            _buyButton.onClick.AddListener(() => BuyClicked?.Invoke(this));
        }

        private void OnDestroy() {
            ShowInfoClicked = null;
            BuyClicked = null;
            Item = null;
        }

        public bool BuyButtonInteractable { get => _buyButton.interactable; set => _buyButton.interactable = value; }
        public ShopItemData Item { get; private set; }

        public void OnItemProcessing() {
            _canvasGroup.interactable = false;
            _buyButtonText.text = PROCESS_TEXT;
        }

        public void OnItemProcessingComplete() {
            _canvasGroup.interactable = true;
            _buyButtonText.text = BUY_TEXT;
        }

        public void Display(ShopItemData item) {
            Item = item;
            _itemName.text = item.ItemName;
        }
    }
}
