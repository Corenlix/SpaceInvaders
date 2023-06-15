using System;
using Code.Levels;
using UniRx;
using Zenject;

namespace Code.Gameplay.Waves
{
    public class WavesHandler : IWaveProvider, IDisposable
    {
        private readonly ReactiveProperty<int> _index = new(-1);
        private readonly ReactiveProperty<Wave> _currentWave = new();
        private readonly IFactory<WaveConfig, Wave> _factory;
        private readonly ILevelLoader _levelLoader;
        private readonly WaveConfig[] _waveConfigs;
        private IDisposable _subscription;

        public IReadOnlyReactiveProperty<int> Index => _index;
        public int WavesCount => _waveConfigs.Length;

        public WavesHandler(WaveConfig[] waveConfigs, IFactory<WaveConfig, Wave> factory, ILevelLoader levelLoader)
        {
            _waveConfigs = waveConfigs;
            _factory = factory;
            _levelLoader = levelLoader;
            Iterate();
        }

        private void Iterate()
        {
            _subscription?.Dispose();
            _index.Value++;
            if (_index.Value >= _waveConfigs.Length) return;

            _currentWave.Value = _factory.Create(_waveConfigs[_index.Value]);
            _subscription = _currentWave.Value.EnemiesRemain.Subscribe(OnEnemiesCountUpdated);
        }

        private void OnEnemiesCountUpdated(int count)
        {
            if (count == 0)
                OnWaveEnded();
        }

        private void OnWaveEnded()
        {
            if (_waveConfigs.Length == _index.Value + 1)
            {
                _levelLoader.LoadNext();
            }
            else
            {
                Iterate();
            }
        }

        public void Dispose()
        {
            _index?.Dispose();
            _subscription?.Dispose();
        }
    }
}