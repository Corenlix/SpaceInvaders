using System;
using UnityEngine;

namespace Code.Gameplay.Waves
{
    [Serializable]
    public class WaveConfig
    {
        [SerializeField] private Wave _prefab;
        
        public Wave Prefab => _prefab;
    }
}