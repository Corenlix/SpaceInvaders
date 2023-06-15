using System;
using Code.Gameplay.Camera;
using Railcar.Time;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Inputs
{
    public class MouseInput : IDestinationInput, IInitializable, IDisposable
    {
        private readonly ITimeObservable _time;
        private readonly ICameraFollower _camera;
        private IDisposable _subscription;
        public Vector2? Destination { get; private set; }

        public MouseInput(
            [Inject(Id = TimeID.Frame)] ITimeObservable time,
            [Inject] ICameraFollower cameraFollower)
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

        private void OnUpdated(float _)
        {
            if (Input.GetMouseButton(0))
            {
                Destination = _camera.Camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Destination = null;
            }
        }
    }
}