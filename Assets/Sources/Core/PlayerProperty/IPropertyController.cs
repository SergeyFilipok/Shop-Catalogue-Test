using System;

namespace Core {
    public interface IPropertyController<T> : IPropertyController {
        new IReadOnlyPlayerProperty<T> Property { get; }
        void Change(PropertyDelta<T> delta);
        bool CanHandle(PropertyDelta<T> delta);
    }

    public interface IPropertyController : IDisposable {
        IPlayerProperty Property { get; }
        void Change(PropertyDelta delta);
        bool CanHandle(PropertyDelta delta);
    }
}
