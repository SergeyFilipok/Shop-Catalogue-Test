using Core;

namespace Health {
    public class HealthInitializer : MonoInitilizable {
        public override void Initialize() {
            var health = GetProperty();
            var controller = new HealthController(health);
            GameApplication.AddController(controller);
            IsInitialized = true;
        }

        private IPlayerProperty<int> GetProperty() {
            return GameApplication.PlayerData.GetOrCreate<int>(HealthController.Health);
        }
    }
}
