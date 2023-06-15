using UnityEngine;
using Zenject;

namespace Code.Levels
{
    [CreateAssetMenu]
    public class LevelsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelData[] _levels;

        public override void InstallBindings()
        {
            Container.Bind<LevelData[]>().FromInstance(_levels).AsSingle();
            Container.BindInterfacesTo<LevelLoader>().AsSingle();
        }
    }
}