using System.Collections;
using UnityEngine;

namespace VIP {
    public class CoroutinesHolder : MonoBehaviour {
        private static CoroutinesHolder _instance;

        public static CoroutinesHolder Instance => GetOrCreate();
        public static bool IsExists => _instance != null;

        public static Coroutine Run(IEnumerator enumerator) {
            return Instance.StartCoroutine(enumerator);
        }

        public static void Stop(Coroutine coroutine) {
            if (IsExists) {
                Instance.StopCoroutine(coroutine);
            }
        }

        private static CoroutinesHolder GetOrCreate() {
            if (_instance == null) {
                var go = new GameObject(nameof(CoroutinesHolder));
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<CoroutinesHolder>();
            }
            return _instance;
        }
    }
}
