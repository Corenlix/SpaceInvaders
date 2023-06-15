using Zenject;

namespace Code.Gameplay.Inputs
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR_WIN
            Container.BindInterfacesAndSelfTo<MouseInput>().AsSingle();
#else
            Container.BindInterfacesAndSelfTo<TapInput>().AsSingle();
#endif
        }
    }
}