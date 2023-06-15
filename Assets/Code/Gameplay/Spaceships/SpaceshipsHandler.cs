using System;
using Railcar.Time;
using Zenject;

namespace Code.Gameplay.Spaceships
{
    public class SpaceshipsHandler : IInitializable, IDisposable
    {
        private readonly ITimeObservable _time;
        private readonly SpaceshipContainer _spaceshipContainer;
        private IDisposable _timeSubscription;

        public SpaceshipsHandler(
            [Inject(Id = TimeID.Frame)] ITimeObservable time,
            [Inject] SpaceshipContainer spaceshipContainer
        )
        {
            _time = time;
            _spaceshipContainer = spaceshipContainer;
        }

        public void Initialize()
        {
            _timeSubscription = _time.Observe(OnUpdated);
        }

        public void Dispose()
        {
            _timeSubscription.Dispose();
        }

        private void OnUpdated(float deltaTime)
        {
            _spaceshipContainer.CurrentShip.Value?.Shoot();
        }
    }
}