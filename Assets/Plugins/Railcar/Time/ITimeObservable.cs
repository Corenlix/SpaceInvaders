using System;
using UniRx;

namespace Railcar.Time
{
    public interface ITimeObservable
    {
        IReadOnlyReactiveProperty<float> Current { get; }
        IDisposable Observe(Action<float> callback);
        IDisposable Mark(float duration, Action<float> callback);
        IDisposable MarkAndRepeat(float duration, Action<float> callback);
        IDisposable MarkAndRepeat(Func<float> duration, Action<float> callback);
    }
}