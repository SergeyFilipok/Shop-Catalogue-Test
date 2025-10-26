using System;

namespace Core {
    public class PlayerProperty<T> : IPlayerProperty<T>, IEquatable<IPlayerProperty<T>> {
        private T _value;
        private event Action<IPlayerProperty> _changed;

        public string ID { get; private set; }
        public T Value { get => _value; set => SetValue(value); }
        object IPlayerProperty.Value { get; }

        public event Action<IReadOnlyPlayerProperty<T>> Changed;
        event Action<IPlayerProperty> IPlayerProperty.Changed {
            add {
                _changed += value;
            }

            remove {
                _changed -= value;
            }
        }

        public PlayerProperty(string id, T defaultValue = default(T)) {
            ID = id;
            _value = defaultValue;
        }


        public bool Equals(IPlayerProperty<T> other) {
            return other.ID == ID && IsEquals(other.Value, _value);
        }

        public override int GetHashCode() {
            return ID.GetHashCode();
        }

        public override string ToString() {
            return $"{ID}:{_value.ToString()}";
        }

        public void Dispose() {
            if (_value is IDisposable disposable) disposable.Dispose();
            Changed = null;
        }

        protected virtual bool IsEquals(T x, T y) {
            if (x is IEquatable<T> xEQ && y is IEquatable<T> yEQ) {
                return xEQ.Equals(yEQ);
            } else {
                return x.Equals(y);
            }
        }

        private void SetValue(T value) {
            if (IsEquals(value, _value) == false) {
                _value = value;
                _changed?.Invoke(this);
                Changed?.Invoke(this);
            }
        }
    }
}
