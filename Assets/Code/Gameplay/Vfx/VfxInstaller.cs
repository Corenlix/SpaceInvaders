using Code.Gameplay.Factories;
using Code.Gameplay.Factories.Pools;
using Zenject;

namespace Code.Gameplay.Vfx
{
    public class VfxInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<AnimationVfx, IPool<AnimationVfx>>()
                .FromFactory<CachedFactory<AnimationVfx, IPool<AnimationVfx>>>();
            Container.BindIFactory<AnimationVfx, IPool<AnimationVfx>>()
                .FromMethod(CreatePool)
                .WhenInjectedInto<CachedFactory<AnimationVfx, IPool<AnimationVfx>>>();
        }

        private IPool<AnimationVfx> CreatePool(DiContainer container, AnimationVfx prefab)
        {
            return container.Instantiate<AnimationVfx.Pool>(new[]
            {
                container.Instantiate<Factories.PrefabFactory<AnimationVfx>>(new[]
                {
                    prefab
                })
            });
        }
    }
}