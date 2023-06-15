using System.Collections.Generic;
using Zenject;

namespace Code.Gameplay.Factories.Pools
{
    public interface IPool<in TParam, TProduct> : IFactory<TParam, TProduct>
    {
        ICollection<TProduct> Entities { get; }
        void Despawn(TProduct entity);
    }

    public interface IPool<T> : IFactory<T>
    {
        ICollection<T> Entities { get; }
        void Despawn(T entity);
    }
}