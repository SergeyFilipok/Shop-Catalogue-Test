using System;

namespace Core {
    public interface IReadOnlyPlayerProperty<T> { 
        T Value { get; }

        event Action<IReadOnlyPlayerProperty<T>> Changed;
    }
}
