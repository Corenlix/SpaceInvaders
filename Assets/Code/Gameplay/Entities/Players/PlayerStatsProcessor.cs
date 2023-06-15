using Code.Gameplay.Entities.Stats;
using Railcar.Time;
using Zenject;

namespace Code.Gameplay.Entities.Players
{
    public class PlayerStatsProcessor : BaseStatsProcessor
    {
        private readonly PlayerStats _stats;

        public float Speed => _stats.Speed;
        public float SmoothMoveCoefficient => _stats.SmoothMoveCoefficient;


        public PlayerStatsProcessor([Inject(Id = TimeID.Frame)] ITimeObservable time, PlayerStats stats) : base(time, stats)
        {
            _stats = stats;
        }
    }
}