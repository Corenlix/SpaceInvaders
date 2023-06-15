using Code.Gameplay.Entities.Players;
using Code.Gameplay.Factories.Pools;
using Railcar.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Items
{
    public abstract class Item : MonoPoolBridge<Item>
    {
        private const float ItemsSpeed = 1f;
        private const float ItemsDisposeTime = 15f/ItemsSpeed;
        private ITimeObservable _time;
        private CompositeDisposable _subscriptions;

        [Inject]
        protected void Inject([Inject(Id = TimeID.Frame)] ITimeObservable time)
        {
            _time = time;
        }

        protected override void OnSpawned()
        {
            gameObject.SetActive(true);
            _subscriptions = new CompositeDisposable(); 
            _subscriptions.Add(_time.Observe(OnUpdated));
            _subscriptions.Add(_time.Mark(ItemsDisposeTime, _ => Dispose()));
        }

        private void OnUpdated(float deltaTime)
        {
            transform.Translate(deltaTime * ItemsSpeed * Vector2.down);
        }

        protected override void OnDespawned()
        {
            if(this)
                gameObject.SetActive(false);
            _subscriptions.Dispose();
        }

        protected abstract void OnPicked();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPicked();
                Dispose();
            }
        }
    }
}