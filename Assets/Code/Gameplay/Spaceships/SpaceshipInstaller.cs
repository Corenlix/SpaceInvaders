using Code.Gameplay.Bullets;
using Code.Gameplay.Factories;
using Code.Gameplay.Factories.Pools;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Spaceships
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [SerializeField] private SpaceshipContainer _spaceshipContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SpaceshipContainer>().FromInstance(_spaceshipContainer).AsSingle();

            Container.BindIFactory<Transform, SpaceshipConfig, ISpaceship>().FromMethod(CreateSpaceship);
        
            Container.BindIFactory<Bullet, IPool<BulletSettings, Bullet>>()
                .FromFactory<CachedFactory<Bullet, IPool<BulletSettings, Bullet>>>();
            Container.BindIFactory<Bullet, IPool<BulletSettings, Bullet>>()
                .FromMethod(CreatePool)
                .WhenInjectedInto<CachedFactory<Bullet, IPool<BulletSettings, Bullet>>>();
        }
    
        private IPool<BulletSettings, Bullet> CreatePool(DiContainer container, Bullet prefab)
        {
            return container.Instantiate<Bullet.Pool>(new []
            {
                container.Instantiate<Factories.PrefabFactory<Bullet>>(new []
                {
                    prefab
                })
            });
        }

        private ISpaceship CreateSpaceship(DiContainer container, Transform parent, SpaceshipConfig spaceshipConfig)
        {
            return container.InstantiatePrefabForComponent<ISpaceship>(spaceshipConfig.Prefab, parent);
        }
    }
}