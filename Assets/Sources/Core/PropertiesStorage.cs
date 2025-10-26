using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core {
    public class PropertiesStorage : IDisposable {
        private Dictionary<string, IPlayerProperty> _dataStorage;

        public IEnumerable<IPlayerProperty> Properties => _dataStorage.Values;

        public PropertiesStorage() {
            _dataStorage = new();
        }

        public void Dispose() {
            _dataStorage.Clear();
            _dataStorage = null;
        }

        public IPlayerProperty<T> GetProperty<T>(string id) {
            if (_dataStorage.TryGetValue(id, out var prop)) {
                if (prop is IPlayerProperty<T> casted) {
                    return casted;
                } else {
                    Debug.LogError($"Property with id: {id} is NOT {typeof(T)}!");
                    return null;
                }
            } else {
                Debug.LogError($"Property with id: {id} doesn't exist!");
                return null;
            }
        }

        public void SetProperty(string id, IPlayerProperty value) {
            _dataStorage[id] = value;
        }
    }
}
