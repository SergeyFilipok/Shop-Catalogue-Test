using Core;
using UnityEngine;

namespace Shopping {
    public class ShopInitializer : MonoInitilizable {
        [SerializeField] private ShopCatalogue _catalogue = null;

        public override void Initialize() {
            var server = CreateServer();
            var shop = new Shop(server, _catalogue);
            GameApplication.RegisterSystem<Shop>(shop);
            IsInitialized = true;
        }

        private INetworkService CreateServer() {
            return new FakeServer(3);
        }
    }
}
