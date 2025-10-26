using Cysharp.Threading.Tasks;
using System.Threading;

namespace Shopping {
    public interface INetworkService {
        UniTask<ServerResponse> SendItemPurchasingRequest(ShopItemData item, CancellationToken cancellation);
    }
}
