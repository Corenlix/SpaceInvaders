using System;
using System.Collections.Generic;

namespace Railcar.Time.Iterators
{
    public abstract class TimeIterator<T> : IDisposable
    {
        private readonly ITimeObservable _time;
        private readonly IEnumerator<T> _enumerator;
        private IDisposable _timerSubscription;

        protected TimeIterator(ITimeObservable time, IEnumerable<T> items)
            : this(time, items.GetEnumerator())
        {
        }

        private TimeIterator(ITimeObservable time, IEnumerator<T> enumerator)
        {
            _time = time;
            _enumerator = enumerator;
        }

        public void Start()
        {
            _enumerator.Reset();
            _timerSubscription?.Dispose();
            OnElapsed(0);
        }

        public void Dispose()
        {
            _timerSubscription?.Dispose();
        }

        protected abstract float GetTimeFromItem(T item);
        protected abstract void OnIterated(T item);

        private void OnElapsed(float currentTime)
        {
            if (_enumerator.MoveNext())
            {
                var item = _enumerator.Current;
                var currentDuration = GetTimeFromItem(item);
                OnIterated(item);
                _timerSubscription = _time.Mark(currentDuration, OnElapsed);
            }
        }
    }
}