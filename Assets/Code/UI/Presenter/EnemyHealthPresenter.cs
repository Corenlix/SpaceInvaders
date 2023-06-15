using Code.Gameplay.Entities.Enemies;
using Code.Gameplay.Entities.Stats;
using Code.UI.View;
using UnityEngine;

namespace Code.UI.Presenter
{
    public class EnemyHealthPresenter : MonoBehaviour
    {
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private HealthView _healthView;

        private void Awake()
        {
            _enemy.EnemyStatsProcessor.HealthChanged += OnHealthChanged;
            _enemy.EnemyStatsProcessor.ShieldChanged += OnShieldChanged;
            UpdateHealth();
            UpdateShield();
        }

        private void OnHealthChanged(IStatsProcessor statsProcessor, float value)
        {
            UpdateHealth();
        }

        private void OnShieldChanged(IStatsProcessor statsProcessor, float value)
        {
            UpdateShield();
        }

        private void UpdateHealth()
        {
            _healthView.UpdateHealth(_enemy.EnemyStatsProcessor.Health, _enemy.EnemyStatsProcessor.MaxHealth);
        }

        private void UpdateShield()
        {
            _healthView.UpdateShield(_enemy.EnemyStatsProcessor.Shield, _enemy.EnemyStatsProcessor.MaxShield);
        }

        private void OnDestroy()
        {
            _enemy.EnemyStatsProcessor.HealthChanged -= OnHealthChanged;
            _enemy.EnemyStatsProcessor.ShieldChanged -= OnShieldChanged;
        }
    }
}