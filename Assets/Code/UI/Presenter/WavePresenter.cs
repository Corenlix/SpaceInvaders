using System;
using Code.Gameplay.Waves;
using Code.UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Presenter
{
    public class WavePresenter : MonoBehaviour
    {
        [SerializeField] private WaveView _waveView;
        private IWaveProvider _waveProvider;
        private IDisposable _subscription;
        
        [Inject]
        private void Inject(IWaveProvider waveProvider)
        {
            _waveProvider = waveProvider;
        }

        private void Awake()
        {
            _subscription = _waveProvider.Index.Subscribe(OnWaveChange);
        }

        private void OnWaveChange(int wave) {
            _waveView.UpdateWave(wave + 1, _waveProvider.WavesCount);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
    }
}