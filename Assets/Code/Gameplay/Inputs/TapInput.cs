using System;
using Code.Gameplay.Camera;
using Railcar.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Inputs
{
    public class TapInput : IInitializable, IDisposable, IDestinationInput
    {
        private readonly ITimeObservable _time;
        private readonly ICameraFollower _camera;
        private IDisposable _subscription;
        public Vector2? Destination { get; private set; }


        public TapInput([Inject(Id = TimeID.Frame)] ITimeObservable time, ICameraFollower cameraFollower)
        {
            _camera = cameraFollower;
            _time = time;
        }

        public void Initialize()
        {
            _subscription = _time.Observe(OnUpdated);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void OnUpdated(float deltaTime)
        {
            if (Input.touchCount > 0)
            {
                Destination = _camera.Camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                Destination = null;
            }
        }
    }
}