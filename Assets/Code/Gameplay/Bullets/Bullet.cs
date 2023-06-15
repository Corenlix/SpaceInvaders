using Code.Gameplay.Entities;
using Code.Gameplay.Factories.Pools;
using Code.Gameplay.Vfx;
using Railcar.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Bullets
{
    public class Bullet : MonoPoolBridge<BulletSettings, Bullet>
    {
        [SerializeField] private AnimationVfx _destroyVfx;
        private CompositeDisposable _subscriptions;
        private ITimeObservable _time;
        private IPool<AnimationVfx> _vfxFactory;

        [Inject]
        private void Inject([Inject(Id = TimeID.Frame)] ITimeObservable time, IFactory<AnimationVfx, IPool<AnimationVfx>> vfxFactory)
        {
            _time = time;
            _vfxFactory = vfxFactory.Create(_destroyVfx);
        }

        private void OnUpdated(float deltaTime)
        {
            transform.Translate(Vector3.up * (Settings.Speed * deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEntityFacade entity) && gameObject.activeSelf)
            {
                entity.StatsProcessor.TakeDamage(Settings.Damage);
                _vfxFactory.Create().transform.position = transform.position;
                Dispose();
            }
        }

        protected override void OnSpawned()
        {
            gameObject.SetActive(true);
            transform.position = Settings.StartPosition;
            transform.rotation = Quaternion.Euler(0, 0, Settings.Angle);
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_time.Observe(OnUpdated));
            _subscriptions.Add(_time.Mark(TimeToDispose, _ => Dispose()));
        }

        protected override void OnDespawned()
        {
            _subscriptions.Dispose();
            if (this)
                gameObject.SetActive(false);
        }

        private float TimeToDispose => 15f / Settings.Speed;
    }
}