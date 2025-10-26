namespace Core {
    public abstract class PropertyDelta<T> : PropertyDelta {
        public abstract new T Value { get; }

        protected override object GetValue() {
            return Value;
        }
    }

    public abstract class PropertyDelta {
        public abstract string TargetID { get; }
        public abstract ChangingActionType ActionType { get; }
        public object Value => GetValue();

        protected abstract object GetValue();

        public override string ToString() {
            return string.Format(GetFormat(ActionType), Value, TargetID);
        }

        private string GetFormat(ChangingActionType actionType) {
            switch (actionType) {
                case ChangingActionType.Add: return "+ {0} {1}";
                case ChangingActionType.AddPercent: return "+ {0}% {1}";
                case ChangingActionType.Remove: return "- {0} {1}";
                case ChangingActionType.RemovePercent: return "- {0}% {1}";
                case ChangingActionType.Set: return "Set {0}";
            }
            return "{0} {1}";
        }
    }
}
