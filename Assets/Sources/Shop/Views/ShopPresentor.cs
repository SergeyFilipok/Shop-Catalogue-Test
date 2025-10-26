using Core;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shopping {
    public class ShopPresentor : MonoBehaviour {
        [SerializeField] private ShopScreen _shopScreen = null;

        private Shop _shop;
        private ShopDetailedInfoPopup _popup;
        private bool _popupIsLoading = false;
        private Dictionary<ShopItemData, ShopDetailedInfoPopup> _cachedPopups;
        private HashSet<string> _idToCheck;

        private void Awake() {
            _shop = GameApplication.GetSystem<Shop>();
            _cachedPopups = new();
            _idToCheck = new HashSet<string>();
            _shopScreen.ItemClicked += OnItemClicked;
            _shopScreen.InfoClicked += OnInfoClicked;

            var properties = GameApplication.PlayerData.Properties;
            foreach (var property in properties) {
                property.Changed += OnPropertyChanged;
            }
        }

        private void Start() {
            _shopScreen.Display(_shop.Catalogue.Items);

            foreach (var view in _shopScreen.Views) {
                var shopItem = view.Item;
                var canBuy = _shop.CanBuy(shopItem);
                view.BuyButtonInteractable = canBuy;

                foreach (var p in shopItem.Prices) {
                    _idToCheck.Add(p.TargetID);
                }
            }
        }

        private void OnPropertyChanged(IPlayerProperty property) {
            var id = property.ID;
            if (_idToCheck.Contains(id) == false) return;

            foreach (var view in _shopScreen.Views) {
                var needUpdate = view.Item.Prices.Any(i => i.TargetID == id);
                if (needUpdate) {
                    var canBuy = _shop.CanBuy(view.Item);
                    view.BuyButtonInteractable = canBuy;
                    if (TryGetPopupFromCache(view.Item, out var popup)) {
                        popup.BuyButtonInteractable = canBuy;
                    }
                }
            }
        }

        private async void OnInfoClicked(ShopItemView view) {
            if (_popupIsLoading == false) {
                if (TryGetPopupFromCache(view.Item, out var popup) == false) {
                    _popupIsLoading = true;
                    var sceneName = view.Item.SceneName;
                    var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    await operation.ToUniTask();
                    var loadedScene = SceneManager.GetSceneByName(sceneName);
                    var roots = loadedScene.GetRootGameObjects();
                    var newPopup = FindInfoPopup(roots);
                    newPopup.Display(view.Item);
                    newPopup.CloseClicked += OnPopupCloseClicked;
                    newPopup.BuyClicked += OnPopupBuyClicked;
                    var canBuy = _shop.CanBuy(view.Item);
                    newPopup.BuyButtonInteractable = canBuy;
                    _cachedPopups.Add(view.Item, newPopup);
                    _popup = newPopup;
                    _popupIsLoading = false;
                } else {
                    _popup = popup;
                }
                _popup.Show();
            }
        }

        private async void OnPopupBuyClicked(ShopDetailedInfoPopup popup) {
            popup.OnItemProcessing();
            await _shop.BuyItem(popup.Item);
            popup.OnItemProcessingComplete();
        }

        private void OnPopupCloseClicked(ShopDetailedInfoPopup popup) {
            popup.Hide();
        }

        private ShopDetailedInfoPopup FindInfoPopup(GameObject[] roots) {
            foreach (var root in roots) {
                var popup = root.GetComponentInChildren<ShopDetailedInfoPopup>();
                if (popup != null) return popup;
            }
            return null;
        }

        private async void OnItemClicked(ShopItemView view) {
            _shopScreen.Interactable = false;
            view.OnItemProcessing();
            await _shop.BuyItem(view.Item);
            _shopScreen.Interactable = true;
            view.OnItemProcessingComplete();
        }

        private bool TryGetPopupFromCache(ShopItemData key, out ShopDetailedInfoPopup popup) {
            return _cachedPopups.TryGetValue(key, out popup);
        }
    }
}
