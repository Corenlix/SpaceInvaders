using System;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Factories.Pools
{
    public abstract class MonoPoolBridge<TEntity> : MonoBehaviour, IDisposable
        where TEntity : MonoPoolBridge<TEntity>
    {
        private Pool _pool;

        protected abstract void OnSpawned();
        protected abstract void OnDespawned();
        
        private void OnDestroy()
        {
            if (_pool != null)
                Dispose();
            else OnDespawned();
        }
        
        public void Dispose()
        {
            _pool?.Despawn((TEntity)this);
        }

        public sealed class Pool : MonoPool<TEntity>
        {
            public Pool(IFactory<TEntity> factory) : base(factory) { }

            protected override void OnSpawned(TEntity entity)
            {
                if (!entity)
                {
                    OnDespawned(entity);
                    return;
                }
                
                base.OnSpawned(entity);
                entity._pool = this;
                entity.OnSpawned();
            }

            protected override void OnDespawned(TEntity entity)
            {
                base.OnDespawned(entity);
                entity._pool = null;
                entity.OnDespawned();
            }
        }
    }
    
    public abstract class MonoPoolBridge<TParam, TEntity> : MonoBehaviour, IDisposable
        where TEntity : MonoPoolBridge<TParam, TEntity>
        where TParam : notnull
    {
        private Pool _pool;

        protected bool IsSpawned => _pool != null;
        protected TParam Settings { get; private set; }

        protected abstract void OnSpawned();
        protected abstract void OnDespawned();

        private void OnDestroy()
        {
            if (_pool != null)
                Dispose();
            else OnDespawned();
        }

        public void Dispose()
        {
            _pool.Despawn((TEntity)this);
        }

        public class Pool : MonoPool<TParam, TEntity>
        {
            public Pool(IFactory<TEntity> factory) : base(factory) { }

            protected override void OnSpawned(TEntity entity, TParam param)
            {
                if (!entity)
                {
                    OnDespawned(entity);
                    return;
                }

                entity._pool = this;
                entity.Settings = param;

                entity.OnSpawned();
                base.OnSpawned(entity, param);
            }

            protected override void OnDespawned(TEntity entity)
            {
                base.OnDespawned(entity);
                entity.OnDespawned();
                
                entity._pool = null;
                entity.Settings = default;
            }
        }
    }
}