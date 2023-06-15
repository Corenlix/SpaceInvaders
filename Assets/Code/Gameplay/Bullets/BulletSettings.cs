using System;
using UnityEngine;

namespace Code.Gameplay.Bullets
{
    [Serializable]
    public struct BulletSettings
    {
        public float Damage { get; set; }
        public float Speed { get; set; }
        public float Angle { get; set; }
        public Vector2 StartPosition { get; set; }
    }
}