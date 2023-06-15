using System;
using UnityEngine;

namespace Code.Gameplay.Entities.Enemies
{
    [Serializable]
    public class EnemyStatsTemplate
    {
        [SerializeField] private float _health = 5f;
        [SerializeField] private float _healthRegenDelay = 1f;
        [SerializeField] private float _healthRegenAmount;

        [SerializeField] private float _shield;
        [SerializeField] private float _shieldRegenDelay = 1f;
        [SerializeField] private float _shieldRegenAmount;

        [SerializeField] private float _armor;
        [SerializeField] private float _collisionDamage;

        public EnemyStats Build()
        {
            return new EnemyStats
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
                CollisionDamage = _collisionDamage,
            };
        }
    }
}