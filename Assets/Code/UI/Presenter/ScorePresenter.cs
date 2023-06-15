using System;
using Code.Gameplay.Score;
using Code.UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Presenter
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        private IScoreStorage _scoreStorage;
        private IDisposable _subscription;

        [Inject]
        private void Inject(IScoreStorage scoreStorage)
        {
            _scoreStorage = scoreStorage;
        }

        private void Awake()
        {
            _subscription = _scoreStorage.Score.Subscribe(OnScoreChanged);
            _scoreView.UpdateScore(_scoreStorage.Score.Value);
        }

        private void OnScoreChanged(int amount)
        {
            _scoreView.UpdateScore(amount);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
    }
}