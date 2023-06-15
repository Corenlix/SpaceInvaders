using Code.Gameplay.Factories;
using Code.Gameplay.Factories.Pools;
using Zenject;

namespace Code.Gameplay.Items
{
    public class ItemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<Item, IPool<Item>>()
                .FromFactory<CachedFactory<Item, IPool<Item>>>();
            Container.BindIFactory<Item, IPool<Item>>()
                .FromMethod(CreatePool)
                .WhenInjectedInto<CachedFactory<Item, IPool<Item>>>();
        }

        private IPool<Item> CreatePool(DiContainer container, Item prefab)
        {
            return container.Instantiate<Item.Pool>(new[]
            {
                container.Instantiate<Factories.PrefabFactory<Item>>(new[]
                {
                    prefab
                })
            });
        }
    }
}