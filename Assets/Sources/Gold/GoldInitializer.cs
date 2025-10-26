using Core;

namespace Gold {
    public class GoldInitializer : MonoInitilizable {
        public override void Initialize() {
            var gold = GetProperty();
            var controller = new GoldController(gold);
            GameApplication.AddController(controller);
            IsInitialized = true;
        }

        private IPlayerProperty<int> GetProperty() {
            return GameApplication.PlayerData.GetOrCreate<int>(GoldController.Gold);
        }
    }
}
