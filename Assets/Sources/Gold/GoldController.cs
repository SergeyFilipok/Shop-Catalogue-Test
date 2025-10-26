using Core;
using System;
using UnityEngine;

namespace Gold {
    public class GoldController : IPropertyController<int> {
        public const string Gold = "Gold";

        private IPlayerProperty<int> _goldProperty;

        public IReadOnlyPlayerProperty<int> Property => _goldProperty;
        IPlayerProperty IPropertyController.Property => _goldProperty;

        public GoldController(IPlayerProperty<int> goldProperty) {
            _goldProperty = goldProperty;
        }

        public void Change(PropertyDelta delta) {
            if (delta is PropertyDelta<int> intDelta) {
                Change(intDelta);
            } else {
                throw new ArgumentException("Parameret delta has wrong type!");
            }
        }

        public bool CanHandle(PropertyDelta delta) {
            if (delta is PropertyDelta<int> intDelta) {
                return CanHandle(intDelta);
            } else {
                throw new ArgumentException("Parameret delta has wrong type!");
            }
        }

        public void Change(PropertyDelta<int> delta) {
            switch (delta.ActionType) {
                case ChangingActionType.Add:
                    _goldProperty.Value += Mathf.Abs(delta.Value);
                    break;
                case ChangingActionType.AddPercent:
                    var aPercent = Mathf.Abs((float)delta.Value / 100.0f);
                    var av = Mathf.RoundToInt((float)_goldProperty.Value * aPercent);
                    _goldProperty.Value += av;
                    break;
                case ChangingActionType.Remove:
                    var removeValue = Mathf.Abs(delta.Value);
                    if (removeValue <= _goldProperty.Value) {
                        _goldProperty.Value -= removeValue;
                    } else {
                        throw new InvalidOperationException("Not enough Gold for removing!");
                    }
                    break;
                case ChangingActionType.RemovePercent:
                    var rPercent = Mathf.Abs((float)delta.Value / 100.0f);
                    var rv = Mathf.RoundToInt((float)_goldProperty.Value * rPercent);
                    if (rv <= _goldProperty.Value) {
                        _goldProperty.Value -= rv;
                    } else {
                        throw new InvalidOperationException("Not enough Gold for removing!");
                    }
                    break;
                case ChangingActionType.Set:
                    if (delta.Value >= 0) {
                        _goldProperty.Value = delta.Value;
                    } else { 
                        throw new InvalidOperationException("Gold value can be below zero!");
                    }
                    break;
            }
        }

        public bool CanHandle(PropertyDelta<int> delta) {
            switch (delta.ActionType) {
                case ChangingActionType.Remove:
                    var removeValue = Mathf.Abs(delta.Value);
                    return removeValue <= _goldProperty.Value;
                case ChangingActionType.RemovePercent:
                    var rPercent = Mathf.Abs(delta.Value);
                    var rv = Mathf.RoundToInt((float)_goldProperty.Value * rPercent);
                    return rv <= _goldProperty.Value;
                case ChangingActionType.Set:
                    return delta.Value >= 0;
            }
            return false;
        }

        public void Dispose() {
            _goldProperty.Dispose();
            _goldProperty = null;
        }
    }
}
