using Core;
using System;
using UnityEngine;

namespace VIP
{
    public class TimeDelta : PropertyDelta<TimeSpan> {
        [SerializeField] private ChangingActionType _action = ChangingActionType.Add;
        [SerializeField] private Time _value = new Time();

        public TimeDelta() {}

        public TimeDelta(TimeSpan value, ChangingActionType action) {
            _value = Convert(value);
            _action = action;
        }

        public override string TargetID => VIPController.VIP;
        public override ChangingActionType ActionType => _action;
        public override TimeSpan Value => Convert(_value);

        private static TimeSpan Convert(Time time) {
            var ts = TimeSpan.FromHours(time.Hours);
            ts = ts + TimeSpan.FromMinutes(time.Minutes);
            ts = ts + TimeSpan.FromSeconds(time.Seconds);
            return ts;
        }

        private static Time Convert(TimeSpan span) {
            var t = new Time();
            t.Hours = Mathf.FloorToInt((float)span.TotalHours);
            t.Minutes = span.Minutes;
            t.Seconds = span.Seconds;
            return t;
        }

        [Serializable]
        private class Time {
            public int Hours;
            public int Minutes;
            public int Seconds;
        }
    }
}
