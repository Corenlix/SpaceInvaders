using UnityEngine;
using Zenject;

namespace Code.Gameplay.Entities.Players
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle();
            Container.BindIFactory<PlayerStats, PlayerStatsProcessor>().FromMethod((container, stats) =>
                container.Instantiate<PlayerStatsProcessor>(new object[] { stats }));
        }
    }
}