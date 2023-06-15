using System;
using UnityEngine;

namespace Code.Gameplay.Entities.Players
{
    [Serializable]
    public class PlayerStatsTemplate
    {
        [SerializeField] private float _health;
        [SerializeField] private float _healthRegenDelay = 1f;
        [SerializeField] private float _healthRegenAmount;

        [SerializeField] private float _shield;
        [SerializeField] private float _shieldRegenDelay = 1f;
        [SerializeField] private float _shieldRegenAmount;

        [SerializeField] private float _armor;

        [SerializeField] private float _speed;
        [SerializeField] private float _smoothMoveCoefficient;

        public PlayerStats Build()
        {
            return new PlayerStats
            {
                Health = _health,
                MaxHealth = _health,
                HealthRegenDelay = _healthRegenDelay,
                HealthRegenAmount = _healthRegenAmount,

                Shield = _shield,
                MaxShield = _shield,
                ShieldRegenDelay = _shieldRegenDelay,
                ShieldRegenAmount = _shieldRegenAmount,

                Armor = 1f - _armor / 100f,
            
                Speed = _speed,
                SmoothMoveCoefficient = _smoothMoveCoefficient,
            };
        }
    }
}