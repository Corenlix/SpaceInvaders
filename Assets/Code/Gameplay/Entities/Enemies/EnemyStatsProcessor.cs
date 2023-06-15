using Code.Gameplay.Entities.Stats;
using Railcar.Time;
using Zenject;

namespace Code.Gameplay.Entities.Enemies
{
    public class EnemyStatsProcessor : BaseStatsProcessor
    {
        private readonly EnemyStats _enemyStats;

        public float CollisionDamage => _enemyStats.CollisionDamage;

        public EnemyStatsProcessor([Inject(Id = TimeID.Frame)] ITimeObservable time, EnemyStats stats) :
            base(time, stats)
        {
            _enemyStats = stats;
        }
    }
}