using System.Collections.Generic;
using UnityEngine;

namespace Shopping {
    [CreateAssetMenu(fileName = "Shop Catalogue", menuName = "Configs/Shop/Catalogue")]
    public class ShopCatalogue : ScriptableObject {
        [SerializeField] private ShopItemData[] _items = null;

        public IReadOnlyList<ShopItemData> Items => _items;
    }
}
