using System;
using UnityEngine;

namespace Code.Gameplay.Bullets
{
    [Serializable]
    public class BulletSettingsTemplate
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;

        public BulletSettings Build(float angle, Vector2 position)
        {
            return new BulletSettings
            {
                Damage = _damage,
                Speed = _speed,
                Angle = angle,
                StartPosition = position,
            };
        }
    }
}