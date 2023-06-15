using Code.Gameplay.Entities.Stats;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Entities.Players
{
    public class Player : MonoBehaviour, IEntityFacade
    {
        [SerializeField] private PlayerStatsTemplate _statsTemplate;
        private ILevelLoader _levelLoader;

        public IStatsProcessor StatsProcessor => PlayerStatsProcessor;
        public PlayerStatsProcessor PlayerStatsProcessor { get; private set; }

        public Vector2 Position => transform.position;

        [Inject]
        private void Inject(IFactory<PlayerStats, PlayerStatsProcessor> statsFactory, ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
            PlayerStatsProcessor = statsFactory.Create(_statsTemplate.Build());
            PlayerStatsProcessor.Died += OnDied;
        }

        public void Move(Vector2 destination, float timeDelta)
        {
            var distance = destination - (Vector2)transform.position;
            var lerpDistance = Vector2.Lerp(Vector2.zero, distance, PlayerStatsProcessor.SmoothMoveCoefficient * timeDelta);
            transform.Translate(Vector2.ClampMagnitude(lerpDistance, PlayerStatsProcessor.Speed * timeDelta));
        }

        private void OnDied(IStatsProcessor _)
        {
            _levelLoader.RestartCurrent();
        }

        private void OnDestroy()
        {
            PlayerStatsProcessor.Died -= OnDied;
            PlayerStatsProcessor.Dispose();
        }
    }
}