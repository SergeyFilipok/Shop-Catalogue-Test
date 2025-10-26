using Core;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shopping {
    [CreateAssetMenu(fileName = "Shop Item", menuName = "Configs/Shop/Shop Item")]
    public class ShopItemData : SerializedScriptableObject {
        [SerializeField] private string _itemName = string.Empty;
        [SerializeField] private string _sceneName = string.Empty;
        [SerializeField] private PropertyDeltaContainer[] _reward = null;
        [SerializeField] private PropertyDeltaContainer[] _price = null;

        public string ItemName => _itemName; 
        public string SceneName => _sceneName; 
        public IEnumerable<PropertyDelta> Rewards => _reward.Select(c => c.GetDelta());
        public IEnumerable<PropertyDelta> Prices => _price.Select(c => c.GetDelta()); 
    }
}
