using Core;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shopping {
    public class ShopDetailedInfoPopup : MonoBehaviour {
        private const string BUY_TEXT = "Buy";
        private const string PROCESS_TEXT = "Processing...";

        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private GraphicRaycaster _graphicRaycaster = null;
        [SerializeField] private Button _closeButton = null;
        [SerializeField] private Button _buyButton = null;
        [SerializeField] private TMP_Text _buyButtonText = null;
        [SerializeField] private TMP_Text _headerText = null;
        [SerializeField] private TMP_Text _rewardsText = null;
        [SerializeField] private TMP_Text _pricesText = null;

        private StringBuilder _rewardsStrings;
        private StringBuilder _pricesStrings;

        public event Action<ShopDetailedInfoPopup> CloseClicked;
        public event Action<ShopDetailedInfoPopup> BuyClicked;

        private void Awake() {
            _rewardsStrings = new StringBuilder();
            _pricesStrings = new StringBuilder();

            _closeButton.onClick.AddListener(() => CloseClicked?.Invoke(this));
            _buyButton.onClick.AddListener(() => BuyClicked?.Invoke(this));
        }

        private void OnDestroy() {
            CloseClicked = null;
            BuyClicked = null;
            Item = null;
            _rewardsStrings.Clear();
            _pricesStrings.Clear();
        }

        public bool BuyButtonInteractable { get => _buyButton.interactable; set => _buyButton.interactable = value; }
        public ShopItemData Item { get; private set; }

        public void OnItemProcessing() {
            _graphicRaycaster.enabled = false;
            _buyButtonText.text = PROCESS_TEXT;
        }

        public void OnItemProcessingComplete() {
            _graphicRaycaster.enabled = true;
            _buyButtonText.text = BUY_TEXT;
        }

        public void Display(ShopItemData item) {
            Item = item;
            _headerText.text = item.ItemName;
            _rewardsText.text = GetRewardsAsText(item.Rewards);
            _pricesText.text = GetPricesAsText(item.Prices);
        }

        public void Show() {
            _canvas.enabled = true;
        }

        public void Hide() { 
            _canvas.enabled = false;
        }

        private string GetRewardsAsText(IEnumerable<PropertyDelta> rewards) {
            _rewardsStrings.Clear();
            _rewardsStrings.Append("Rewards:\n");
            foreach (var reward in rewards) { 
                _rewardsStrings.AppendLine(reward.ToString());
            }
            return _rewardsStrings.ToString();
        }

        private string GetPricesAsText(IEnumerable<PropertyDelta> prices) {
            _pricesStrings.Clear();
            _pricesStrings.Append("Prices:\n");
            foreach (var price in prices) {
                _pricesStrings.AppendLine(price.ToString());
            }
            return _pricesStrings.ToString();
        }
    }
}
