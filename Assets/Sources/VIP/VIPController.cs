using Core;
using System;
using System.Collections;
using UnityEngine;

namespace VIP {
    public class VIPController : IPropertyController<TimeSpan> {
        public const string VIP = "vip";

        private IPlayerProperty<TimeSpan> _time;
        private Coroutine _ticking;

        public IReadOnlyPlayerProperty<TimeSpan> Property => _time;
        IPlayerProperty IPropertyController.Property => _time;

        public VIPController(IPlayerProperty<TimeSpan> time) {
            _time = time;
            _ticking = CoroutinesHolder.Run(TimerTick());
        }

        public bool CanHandle(PropertyDelta<TimeSpan> delta) {
            switch (delta.ActionType) {
                case ChangingActionType.AddPercent:
                    return false;
                case ChangingActionType.Remove:
                    return delta.Value.Ticks <= _time.Value.Ticks;
                case ChangingActionType.RemovePercent:
                    return false;
                case ChangingActionType.Set:
                    return delta.Value >= TimeSpan.Zero;
            }
            return false;
        }

        public bool CanHandle(PropertyDelta delta) {
            if (delta is PropertyDelta<TimeSpan> timeDelta) {
                return CanHandle(timeDelta);
            } else {
                throw new ArgumentException("Parameret delta has wrong type!");
            }
        }

        public void Change(PropertyDelta<TimeSpan> delta) {
            switch (delta.ActionType) {
                case ChangingActionType.Add:
                    _time.Value = _time.Value.Add(delta.Value);
                    break;
                case ChangingActionType.AddPercent:
                    throw new InvalidOperationException("Unsupported action type!");
                case ChangingActionType.Remove:
                    if (delta.Value <= _time.Value) {
                        _time.Value = _time.Value.Subtract(delta.Value);
                    }
                    break;
                case ChangingActionType.RemovePercent:
                    throw new InvalidOperationException("Unsupported action type!");
                case ChangingActionType.Set:
                    if (delta.Value >= TimeSpan.Zero) {
                        _time.Value = delta.Value;
                    }
                    break;
            }
        }

        public void Change(PropertyDelta delta) {
            if (delta is PropertyDelta<TimeSpan> timeDelta) {
                Change(timeDelta);
            } else {
                throw new ArgumentException("Parameret delta has wrong type!");
            }
        }

        public void Dispose() {
            _time.Dispose();
            _time = null;
            CoroutinesHolder.Stop(_ticking);
        }

        private IEnumerator TimerTick() {
            while (true) {
                yield return new WaitForSecondsRealtime(1);

                if (_time.Value.TotalSeconds > 1) {
                    _time.Value = _time.Value.Subtract(TimeSpan.FromSeconds(1));
                } else {
                    _time.Value = TimeSpan.Zero;
                }
            }
        }
    }
}
