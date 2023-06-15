using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay.Factories
{
    public class PrefabFactory<T> : IFactory<T>, IValidatable where T : Object
    {
        private readonly DiContainer _container;
        private readonly T _prefab;
        private readonly Transform _parent;

        public PrefabFactory(DiContainer container, T prefab, Transform parent = null)
        {
            _container = container;
            _prefab = prefab;
            if (parent == null)
                parent = new GameObject($"{prefab.name}'s pool").transform;
            _parent = parent;
        }

        public T Create()
        {
            return _container.InstantiatePrefabForComponent<T>(_prefab, _parent);
        }

        public void Validate()
        {
            _container.Instantiate<T>();
        }
    }
}