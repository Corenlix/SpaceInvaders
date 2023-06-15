using System;
using Railcar.Time.Subscriptions;
using UnityEngine;

namespace Railcar.Time.Mono
{
    public class PausableTimeWrapper : MonoBehaviour, ITimeLord
    {
        private readonly TimeObservable _fixedTime = new();
        private readonly TimeObservable _frameTime = new();
        private readonly TimeObservable _lateFrameTime = new();

        private bool _doesFlow;

        public bool DoesFlow
        {
            get => _doesFlow;
            set
            {
                _doesFlow = value;
                FlowingUpdated?.Invoke(this, _doesFlow);
            }
        }

        public event EventHandler<bool> FlowingUpdated;
        public ITimeObservable FixedTime => _fixedTime;
        public ITimeObservable FrameTime => _frameTime;
        public ITimeObservable LateFrameTime => _lateFrameTime;

        private void FixedUpdate()
        {
            UpdateTime(_fixedTime, UnityEngine.Time.fixedDeltaTime);
        }

        private void Update()
        {
            UpdateTime(_frameTime, UnityEngine.Time.deltaTime);
        }

        private void LateUpdate()
        {
            UpdateTime(_lateFrameTime, UnityEngine.Time.deltaTime);
        }

        private void OnDestroy()
        {
            _fixedTime.Dispose();
            _frameTime.Dispose();
            _lateFrameTime.Dispose();
        }

        private void UpdateTime(TimeObservable timeObservable, float timeDelta)
        {
            if (!DoesFlow)
                return;

            timeObservable.Update(timeDelta);
        }
    }
}