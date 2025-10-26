using Core;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Shopping {
    public class FakeServer : INetworkService {
        private readonly float _responseDelay;

        public FakeServer(float responseDelay) {
            _responseDelay = responseDelay;
        }

        public UniTask<ServerResponse> SendItemPurchasingRequest(ShopItemData item, CancellationToken cancellation) {
            var completion = new UniTaskCompletionSource<ServerResponse>();
            UniTask.Delay(GetMilliseconds(_responseDelay), cancellationToken: cancellation)
                .SuppressCancellationThrow()
                .ContinueWith(isCanceled => {
                    if (isCanceled) {
                        completion.TrySetCanceled();
                    } else {
                        var canBuyItem = CheckProperties(item);
                        if (canBuyItem) {
                            completion.TrySetResult(ServerResponse.FromSuccess());
                        } else {
                            completion.TrySetResult(ServerResponse.FromInsufficientFunds());
                        }
                    }
                });
            return completion.Task;
        }

        private bool CheckProperties(ShopItemData item) {
            foreach (var price in item.Prices) {
                var controller = GameApplication.GetController(price.TargetID);
                if (controller == null || controller.CanHandle(price) == false) return false;
            }
            return true;
        }

        private static int GetMilliseconds(float seconds) {
            return Mathf.RoundToInt(seconds * 1000);
        }
    }
}
