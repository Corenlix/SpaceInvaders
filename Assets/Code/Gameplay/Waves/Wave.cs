using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Entities.Enemies;
using UniRx;
using UnityEngine;

namespace Code.Gameplay.Waves
{
    public class Wave : MonoBehaviour
    {
        public IObservable<int> EnemiesRemain => _enemiesRemain;
        private readonly ReactiveProperty<int> _enemiesRemain = new();
        private readonly CompositeDisposable _disposable = new();
        private List<BaseEnemy> _enemies;

        private void Awake()
        {
            _enemies = transform.GetComponentsInChildren<BaseEnemy>().ToList();
            _enemiesRemain.Value = _enemies.Count;
            if (_enemiesRemain.Value == 0)
                throw new InvalidOperationException("No enemies in wave");

            foreach (var enemy in _enemies)
            {
                _disposable.Add(enemy.Died.Subscribe(OnEnemyDied));
            }
        }

        public void OnGetReady()
        {
            _enemies.ForEach(x => x.OnGetReady());
        }

        public void OnGetUnready()
        {
            _enemies.ForEach(x => x.OnGetUnready());
        }
        
        private void OnEnemyDied(BaseEnemy enemy)
        {
            _enemiesRemain.Value--;
            _enemies.Remove(enemy);
        }

        private void OnDestroy()
        {
            _enemiesRemain?.Dispose();
            _disposable?.Dispose();
        }
    }
}