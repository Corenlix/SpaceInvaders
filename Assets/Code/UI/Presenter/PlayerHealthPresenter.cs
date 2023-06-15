using Code.Gameplay.Entities.Players;
using Code.Gameplay.Entities.Stats;
using Code.UI.View;
using UnityEngine;
using Zenject;

namespace Code.UI.Presenter
{
    public class PlayerHealthPresenter : MonoBehaviour
    {
        [SerializeField] private HealthView _healthView;
        private Player _player;

        [Inject]
        private void Inject(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _player.PlayerStatsProcessor.HealthChanged += OnHealthChanged;
            _player.PlayerStatsProcessor.ShieldChanged += OnShieldChanged;
            UpdateHealth();
            UpdateShield();
        }

        private void UpdateHealth()
        {
            _healthView.UpdateHealth(_player.PlayerStatsProcessor.Health, _player.PlayerStatsProcessor.MaxHealth);
        }

        private void UpdateShield()
        {
            _healthView.UpdateShield(_player.PlayerStatsProcessor.Shield, _player.PlayerStatsProcessor.MaxShield);
        }

        private void OnHealthChanged(IStatsProcessor statsProcessor, float value)
        {
            UpdateHealth();
        }

        private void OnShieldChanged(IStatsProcessor statsProcessor, float value)
        {
            UpdateShield();
        }

        private void OnDestroy()
        {
            _player.PlayerStatsProcessor.HealthChanged -= OnHealthChanged;
            _player.PlayerStatsProcessor.ShieldChanged -= OnShieldChanged;
        }
    }
}