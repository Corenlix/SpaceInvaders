using Code.Gameplay.Entities.Enemies;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Waves
{
    [CreateAssetMenu(menuName = "Waves/Compilation", fileName = "New WavesCompilation")]
    public class WavesInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private WaveConfig[] _waves;

        public override void InstallBindings()
        {
            Container.Bind<WaveConfig[]>()
                .FromInstance(_waves)
                .AsSingle();

            Container.BindIFactory<WaveConfig, Wave>().FromMethod(CreateWave);

            Container.BindIFactory<EnemyStats, EnemyStatsProcessor>().FromMethod(CreateEnemyStats);
        }

        private EnemyStatsProcessor CreateEnemyStats(DiContainer container, EnemyStats stats)
        {
            return container.Instantiate<EnemyStatsProcessor>(new object[] { stats });
        }
        private Wave CreateWave(DiContainer container, WaveConfig config)
        {
            return container.InstantiatePrefabForComponent<Wave>(config.Prefab);
        }
    }
}