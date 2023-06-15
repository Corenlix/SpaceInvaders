using Code.Gameplay.Inputs;
using Code.Gameplay.Spaceships;
using Code.Gameplay.Waves;
using Railcar.Time;
using Zenject;

namespace Code.Gameplay.Startup
{
    public class Bootstrapper : MonoInstaller
    {
        private ITimeLord _timeLord;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpaceshipsHandler>().AsSingle();
            Container.BindInterfacesTo<WavesHandler>().AsSingle();
        }

        [Inject]
        private void Inject(ITimeLord timeLord)
        {
            _timeLord = timeLord;
        }

        public override void Start()
        {
            _timeLord.DoesFlow = true;
        }
    }
}