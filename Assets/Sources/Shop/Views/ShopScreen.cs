using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Shopping {
    public class ShopScreen : MonoBehaviour {
        [SerializeField] private GraphicRaycaster _graphicRaycaster = null;
        [SerializeField] private RectTransform _itemsContainer = null;
        [SerializeField] private ShopItemView _itemPrefab = null;
        private List<ShopItemView> _views;

        public bool Interactable { get => _graphicRaycaster.enabled; 
            set => _graphicRaycaster.enabled = value; }
        public IReadOnlyList<ShopItemView> Views => _views;

        public event Action<ShopItemView> ItemClicked;
        public event Action<ShopItemView> InfoClicked;

        private void OnDestroy() {
            ItemClicked = null;
            InfoClicked = null; 
        }

        public void Display(IEnumerable<ShopItemData> items) {
            _views = new List<ShopItemView>(items.Count());
            foreach (var item in items) { 
                var view = Instantiate(_itemPrefab, _itemsContainer);
                view.BuyClicked += OnItemBuyClicked;
                view.ShowInfoClicked += OnItemInfoClicked;
                view.Display(item);
                _views.Add(view);
            }
        }

        private void OnItemInfoClicked(ShopItemView item) {
            InfoClicked?.Invoke(item);
        }

        private void OnItemBuyClicked(ShopItemView item) {
            ItemClicked?.Invoke(item);
        }
    }
}
