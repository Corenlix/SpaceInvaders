using Zenject;

namespace Code.Gameplay.Score
{
    public class StorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IScoreStorage>().To<ScoreStorage>().AsSingle();
        }
    }
}