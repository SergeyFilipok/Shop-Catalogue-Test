using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Shopping {
    public class Shop : IDisposable {
        private readonly CancellationTokenSource _mainCancellation;
        private readonly INetworkService _server;
        private readonly ShopCatalogue _catalogue;

        public ShopCatalogue Catalogue => _catalogue;

        public Shop(INetworkService server, ShopCatalogue catalogue) {
            _mainCancellation = new CancellationTokenSource();
            _server = server;
            _catalogue = catalogue;
        }

        public bool CanBuy(ShopItemData item) {
            foreach (var price in item.Prices) {
                var controller = GameApplication.GetController(price.TargetID);
                if (controller == null || controller.CanHandle(price) == false) return false;
            }
            return true;
        }

        public async UniTask BuyItem(ShopItemData item) { 
            var taskResult = await _server.SendItemPurchasingRequest(item, _mainCancellation.Token)
                .SuppressCancellationThrow();
            if (taskResult.IsCanceled == false) {
                var response = taskResult.Result;
                if (response.success) {
                    HandleItem(item);
                } else {
                   Debug.LogError($"response code: {response.stusCode} \n message {response.message}");
                }
            }
        }

        public void Dispose() {
            _mainCancellation.Cancel();
            _mainCancellation.Dispose();
        }

        //so, actually we don't know what exactly check the server
        //that meens we have to check our properties again on the client
        private void HandleItem(ShopItemData item) {
            if (CanBuy(item)) {
                Pay(item);
                GainRewards(item);
            }
        }

        private void Pay(ShopItemData item) {
            foreach (var price in item.Prices) {
                var controller = GameApplication.GetController(price.TargetID);
                if (controller == null) throw new NullReferenceException($"No controller with ID {price.TargetID}");
                controller.Change(price);
            }
        }

        private void GainRewards(ShopItemData item) {
            foreach (var reward in item.Rewards) {
                var controller = GameApplication.GetController(reward.TargetID);
                if (controller == null) throw new NullReferenceException($"No controller with ID {reward.TargetID}");
                controller.Change(reward);
            }
        }
    }
}
