using System;
using System.Collections.Generic;

namespace Railcar.Time.Subscriptions
{
    internal class RepeatingTimer : Timer
    {
        private readonly Func<float> _intervalFunc;

        public RepeatingTimer(LinkedList<Timer> list, float time, Func<float> intervalFunc, Action<float> callback)
            : base(list, time + intervalFunc.Invoke(), callback)
        {
            _intervalFunc = intervalFunc;
        }

        protected override void OnElapsed(float time)
        {
            // TODO: check disposing more accurately
            if (Node == null)
                return;

            var list = Node.List;
            list.Remove(Node);

            float interval = _intervalFunc.Invoke();
            if (interval <= 0) throw new InvalidOperationException("Interval should be positive");
            Timestamp = time + interval;
            Insert(Node, list);
        }
    }
}