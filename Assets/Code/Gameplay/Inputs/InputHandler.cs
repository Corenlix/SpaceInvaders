using System;
using Code.Gameplay.Entities.Players;
using Railcar.Time;
using Zenject;

namespace Code.Gameplay.Inputs
{
    public class InputHandler : IInitializable, IDisposable
    {
        private readonly Player _player;
        private readonly IDestinationInput _input;
        private readonly ITimeObservable _time;
        private IDisposable _timeSubscription;

        public InputHandler(
            [Inject(Id = TimeID.Frame)] ITimeObservable time,
            [Inject] Player player,
            [Inject] IDestinationInput input
        )
        {
            _player = player;
            _time = time;
            _input = input;
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
            if (_input.Destination.HasValue)
                _player.Move(_input.Destination.Value, deltaTime);
        }
    }
}