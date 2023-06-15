using UniRx;

namespace Code.Gameplay.Waves
{
    public interface IWaveProvider
    {
        IReadOnlyReactiveProperty<int> Index { get; }
        int WavesCount { get; }
    }
}