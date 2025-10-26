using UnityEngine;

namespace Core {
    public abstract class MonoInitilizable : MonoBehaviour, IInitializable {
        public bool IsInitialized { get; protected set; }

        public abstract void Initialize();
    }
}
