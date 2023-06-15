using System;
using Code.Gameplay.Bullets;
using Code.Gameplay.Factories.Pools;
using Railcar.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Spaceships
{
    public class BulletGunnerShip : BaseSpaceship
    {
        [SerializeField] private float _reloadTime;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletSettingsTemplate _bulletSettings;
        [SerializeField] private FirePoint[] _firePoints;
        private ITimeObservable _time;
        private IPool<BulletSettings, Bullet> _pool;
        private bool _isReloading;
        private IDisposable _subscription;

        [Inject]
        private void Inject([Inject(Id = TimeID.Frame)] ITimeObservable time,
            IFactory<Bullet, IPool<BulletSettings, Bullet>> factory)
        {
            _time = time;
            _pool = factory.Create(_bulletPrefab);
        }

        public override void Shoot()
        {
            if (_isReloading) return;
            _isReloading = true;
            _subscription = _time.Mark(_reloadTime, _ => OnReload());
            foreach (var firePoint in _firePoints)
            {
                _pool.Create(_bulletSettings.Build(
                    Vector2.Angle(Vector2.up, transform.up) + firePoint.ShootAngle,
                    firePoint.transform.position));
            }
        }

        private void OnReload()
        {
            _isReloading = false;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}