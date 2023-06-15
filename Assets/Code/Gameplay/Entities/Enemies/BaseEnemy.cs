using Code.Gameplay.Entities.Players;
using Code.Gameplay.Entities.Stats;
using Code.Gameplay.Factories.Pools;
using Code.Gameplay.Items;
using Code.Gameplay.Score;
using Code.Gameplay.Vfx;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Entities.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour, IEntityFacade
    {
        [SerializeField] private EnemyStatsTemplate _enemyStatsTemplate;
        [SerializeField] private AnimationVfx _deathVfx;
        [SerializeField] private DropConfig _dropConfig;
        [SerializeField] private int _scoreReward = 1;
        private IFactory<Item, IPool<Item>> _itemFactory;
        private IPool<AnimationVfx> _deathVfxFactory;
        private IScoreStorage _scoreStorage;
        
        public readonly Subject<BaseEnemy> Died = new();
        public IStatsProcessor StatsProcessor => EnemyStatsProcessor;
        public EnemyStatsProcessor EnemyStatsProcessor { get; private set; }

        public Vector2 Position => transform.position;

        [Inject]
        private void Inject(IFactory<EnemyStats, EnemyStatsProcessor> statsFactory, IScoreStorage scoreStorage,
            IFactory<Item, IPool<Item>> itemFactory, IFactory<AnimationVfx, IPool<AnimationVfx>> deathVfxFactory)
        {
            EnemyStatsProcessor = statsFactory.Create(_enemyStatsTemplate.Build());
            _scoreStorage = scoreStorage;
            _itemFactory = itemFactory;
            EnemyStatsProcessor.Died += OnDied;
            _deathVfxFactory = deathVfxFactory.Create(_deathVfx);
        }

        public abstract void OnGetReady();

        public abstract void OnGetUnready();

        private void OnDied(IStatsProcessor statsProcessor)
        {
            Died.OnNext(this);
            if (_dropConfig)
                _dropConfig.Spawn(_itemFactory, transform.position);
            _deathVfxFactory.Create().transform.position = transform.position;
            _scoreStorage.Obtain(_scoreReward);
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.StatsProcessor.TakeDamage(EnemyStatsProcessor.CollisionDamage);
                EnemyStatsProcessor.Kill();
            }
        }

        protected virtual void OnDestroy()
        {
            EnemyStatsProcessor.Died -= OnDied;
            EnemyStatsProcessor?.Dispose();
            Died?.Dispose();
        }
    }
}