using System;
using Railcar.Time;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.View
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private float _viewTime = 3f;
        private IDisposable _subscription;
        private ITimeObservable _time;
        
        [Inject]
        private void Inject([Inject(Id = TimeID.Frame)] ITimeObservable time)
        {
            _time = time;
        }
        
        public void UpdateWave(int number, int max)
        {
            _subscription?.Dispose();
            _waveText.enabled = true;
            _waveText.text = $"Wave {number}/{max}";
            _subscription = _time.Mark(_viewTime, _ => Hide());
        }

        private void Hide()
        {
            _waveText.enabled = false;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}