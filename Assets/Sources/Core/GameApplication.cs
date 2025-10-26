using System;
using System.Collections.Generic;

namespace Core {
    public class GameApplication : IDisposable {
        private static GameApplication _instance;
        public static bool IsExists => _instance != null;
        public static PropertiesStorage PlayerData => _instance._playerData;

        public static GameApplication Create() {
            if (_instance == null) {
                _instance = new GameApplication();
            } else {
                throw new System.InvalidOperationException("Instance of GameApplication alrady exists!");
            }
            return _instance;
        }

        public static void Clear() {
            _instance.Dispose();
            _instance = null;
        }

        public static void AddController(IPropertyController controller) {
            _instance._controllers.Add(controller.Property.ID, controller);
        }

        public static bool RemoveController(string id) {
            return _instance._controllers.Remove(id);
        }

        public static IPropertyController GetController(string id) {
            _instance._controllers.TryGetValue(id, out IPropertyController controller);
            return controller;
        }

        public static void RegisterSystem<T>(object systemInstance) {
            _instance._systems.Add(typeof(T), systemInstance);
        }

        public static void UnregisterSystem<T>() {
            _instance._systems.Remove(typeof(T));
        }

        public static T GetSystem<T>() {
            if (_instance._systems.TryGetValue(typeof(T), out var system)) {
                return (T)system;
            } else {
                return default(T);
            }
        }

        private PropertiesStorage _playerData;
        private Dictionary<string, IPropertyController> _controllers;
        private Dictionary<Type, object> _systems;

        private GameApplication() {
            _playerData = new PropertiesStorage();
            _controllers = new();
            _systems = new();
        }

        public void Dispose() {
            _playerData.Dispose();
            _controllers.Clear();
            _controllers = null;
            _systems.Clear();
            _systems = null;
        }
    }
}
