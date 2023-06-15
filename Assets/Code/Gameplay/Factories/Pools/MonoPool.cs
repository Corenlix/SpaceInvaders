using UnityEngine;
using Zenject;

namespace Code.Gameplay.Factories.Pools
{
    public class MonoPool<TProduct> : BasePool<TProduct>
        where TProduct : Component
    {
        private readonly IFactory<TProduct> _factory;

        public MonoPool(IFactory<TProduct> factory)
        {
            _factory = factory;
        }

        protected override TProduct Instantiate()
        {
            return _factory.Create();
        }

        protected override void OnSpawned(TProduct entity)
        {
            if (!entity)
            {
                OnDespawned(entity);
                return;
            }
            
            entity.gameObject.SetActive(true);
        }

        protected override void OnDespawned(TProduct entity)
        {
            if (entity)
                entity.gameObject.SetActive(false);
        }
    }
    public class MonoPool<TParam, TProduct> : BasePool<TParam, TProduct>
        where TProduct : Component
    {
        private readonly IFactory<TProduct> _factory;

        public MonoPool(IFactory<TProduct> factory)
        {
            _factory = factory;
        }

        protected sealed override TProduct Instantiate()
        {
            return _factory.Create();
        }

        protected override void OnSpawned(TProduct entity, TParam param)
        {
            if (!entity)
            {
                OnDespawned(entity);
                return;
            }
            entity.gameObject.SetActive(true);
        }

        protected override void OnDespawned(TProduct entity)
        {
            if (entity)
                entity.gameObject.SetActive(false);
        }
    }
}