using System;
using UnityEngine;

namespace Code.Gameplay.Items
{
    [Serializable]
    public class DropData
    {
        [SerializeField] private float _chance;
        [SerializeField] private Item _itemPrefab;

        public float Chance => _chance;
        public Item Prefab => _itemPrefab;
    }
}