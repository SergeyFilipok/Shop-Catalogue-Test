namespace Core {
    public class GameApplicationInitializer : MonoInitilizable {
        private void Awake() {
            DontDestroyOnLoad(this);
        }

        private void OnDestroy() {
            GameApplication.Clear();
        }

        public override void Initialize() {
            GameApplication.Create();
            IsInitialized = true;
        }
    }
}
