using Code.Gameplay.Factories.Pools;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Items
{
    [CreateAssetMenu]
    public class DropConfig : ScriptableObject
    {
        [SerializeField] private DropData[] _items;
        [SerializeField] private bool _onlyOneItem = true;

        public void Spawn(IFactory<Item, IPool<Item>> factory, Vector2 position)
        {
            foreach (var item in _items)
            {
                if (Random.value * 100 <= item.Chance)
                {
                    var pool = factory.Create(item.Prefab);
                    var itemInstance = pool.Create();
                    itemInstance.transform.position = position;
                    if (_onlyOneItem) return;
                }
            }
        }
    }
}