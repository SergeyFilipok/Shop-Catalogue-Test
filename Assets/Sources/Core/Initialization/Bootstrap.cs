using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class Bootstrap : MonoBehaviour {
        [Header("This is the name of the next scene after initialization")]
        [SerializeField] private string _nextScene = "Shop";
        [SerializeField] private MonoInitilizable[] _initializers = new MonoInitilizable[0];

        private void Start() {
            StartCoroutine(Initialization());
        }

        private IEnumerator Initialization() {
            for (int i = 0; i < _initializers.Length; i++) {
                var item = _initializers[i];
                item.Initialize();
                yield return new WaitUntil(() => item.IsInitialized);
            }

            SceneManager.LoadSceneAsync(_nextScene);
        }
    }
}
