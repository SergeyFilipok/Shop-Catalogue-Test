using System;

namespace Core {
    public interface IPlayerProperty : IDisposable {
        string ID { get; }
        object Value { get; }

        event Action<IPlayerProperty> Changed;
    }

    public interface IPlayerProperty<T> : IPlayerProperty, IReadOnlyPlayerProperty<T> {
        new T Value { get; set; }
    }
}
