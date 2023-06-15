using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Levels
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private AssetReference _scene;
        public AssetReference Scene => _scene;
    }
}