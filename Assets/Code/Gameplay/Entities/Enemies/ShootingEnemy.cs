using Code.Gameplay.Bullets;
using Code.Gameplay.Factories.Pools;
using Code.Gameplay.Spaceships;
using Railcar.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Entities.Enemies
{
    public class ShootingEnemy : BaseEnemy
    {
        [SerializeField] private float _reloadTime;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletSettingsTemplate _bulletSettings;
        [SerializeField] private FirePoint[] _firePoints;
        private readonly CompositeDisposable _subscriptions = new();
        private ITimeObservable _time;
        private IPool<BulletSettings, Bullet> _pool;
        private bool _isReloading;
        private bool _shootingEnabled;

        [Inject]
        private void Inject([Inject(Id = TimeID.Frame)] ITimeObservable time,
            IFactory<Bullet, IPool<BulletSettings, Bullet>> factory)
        {
            _time = time;
            _pool = factory.Create(_bulletPrefab);
            _subscriptions.Add(_time.Observe(OnUpdated));
        }

        public override void OnGetReady()
        {
            _shootingEnabled = true;
        }

        public override void OnGetUnready()
        {
            _shootingEnabled = false;
        }

        private void Shoot()
        {
            if (_isReloading || !_shootingEnabled) return;
            _isReloading = true;
            _subscriptions.Add(_time.Mark(_reloadTime, _ => OnReload()));
            foreach (var firePoint in _firePoints)
            {
                _pool.Create(_bulletSettings.Build(
                    Vector2.Angle(Vector2.up, transform.up) + firePoint.ShootAngle,
                    firePoint.transform.position));
            }
        }

        private void OnUpdated(float deltaTime)
        {
            Shoot();
        }

        private void OnReload()
        {
            _isReloading = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _subscriptions?.Dispose();
        }
    }
}