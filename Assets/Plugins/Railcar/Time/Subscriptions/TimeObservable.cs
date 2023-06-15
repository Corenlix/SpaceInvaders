using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UniRx;

namespace Railcar.Time.Subscriptions
{
    public class TimeObservable : ITimeObservable, IDisposable
    {
        private readonly LinkedList<Timer> _timers = new();
        private readonly LinkedList<Stopwatch> _stopwatches = new();
        private readonly ReactiveProperty<float> _current = new();
        public IReadOnlyReactiveProperty<float> Current => _current;

        public void Update(float timeDelta)
        {
            _current.Value += timeDelta;

            var exceptions = new Lazy<List<Exception>>();

            var stopwatch = _stopwatches.First;
            while (stopwatch != null)
            {
                try
                {
                    stopwatch.Value.Update(timeDelta);
                }
                catch (Exception exception)
                {
                    exceptions.Value.Add(exception);
                }

                stopwatch = stopwatch.Next;
            }

            var timer = _timers.First;
            while (timer != null)
            {
                try
                {
                    timer.Value.Update(_current.Value);
                }
                catch (Exception exception)
                {
                    exceptions.Value.Add(exception);
                }
                
                if (timer == _timers.First)
                    break;
                timer = _timers.First;
            }

            if (exceptions.IsValueCreated)
                throw new AggregateException(exceptions.Value);
        }

        public IDisposable MarkAndRepeat(float time, [NotNull] Action<float> callback)
        {
            if (time <= 0)
                throw new InvalidOperationException("Marking time must be positive");

            var timer = new RepeatingTimer(_timers, _current.Value, () => time, callback);
            return timer;
        }

        public IDisposable MarkAndRepeat([NotNull] Func<float> timeFunc, [NotNull] Action<float> callback)
        {
            var timer = new RepeatingTimer(_timers, _current.Value, timeFunc, callback);
            return timer;
        }

        public IDisposable Mark(float time, [NotNull] Action<float> callback)
        {
            if (time <= 0)
                throw new InvalidOperationException("Marking time must be positive");

            var timestamp = _current.Value + time;
            var timer = new Timer(_timers, timestamp, callback);
            return timer;
        }

        public IDisposable Observe([NotNull] Action<float> callback)
        {
            var stopwatch = new Stopwatch(_stopwatches, callback);
            return stopwatch;
        }

        public void Dispose()
        {
            DisposeList(_timers);
            DisposeList(_stopwatches);
        }

        private static void DisposeList<T>(LinkedList<T> list)
            where T : IDisposable
        {
            var count = list.Count;
            while (count != 0)
            {
                list.First.Value.Dispose();

                if (count == list.Count)
                    throw new InvalidOperationException("Disposing subscription must delete it from the list");

                count = list.Count;
            }
        }
    }
}