namespace Core {
    public static class PropertiesStorageExtensions {
        public static IPlayerProperty<T> GetOrCreate<T>(this PropertiesStorage storage, string id) {
            var p = storage.GetProperty<T>(id);
            if (p == null) {
                p = new PlayerProperty<T>(id);
                storage.SetProperty(id, p);
            }
            return p;
        }
    }
}
