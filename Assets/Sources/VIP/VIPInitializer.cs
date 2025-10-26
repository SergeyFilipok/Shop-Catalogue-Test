using Core;
using System;

namespace VIP {
    public class VIPInitializer : MonoInitilizable {
        public override void Initialize() {
            var time = GetProperty();
            var controller = new VIPController(time);
            GameApplication.AddController(controller);
            IsInitialized = true;
        }

        private IPlayerProperty<TimeSpan> GetProperty() {
            return GameApplication.PlayerData.GetOrCreate<TimeSpan>(VIPController.VIP);
        }
    }
}
