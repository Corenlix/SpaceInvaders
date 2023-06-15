using System;
using UnityEngine;

namespace Code.Gameplay.Spaceships
{
    [Serializable]
    public class SpaceshipConfig
    {
        [SerializeField] private BaseSpaceship _prefab;
        public BaseSpaceship Prefab => _prefab;
    }
}