using System;
using UnityEngine;

namespace Code.Gameplay.Vfx
{
    public class AnimationVfx : Vfx<AnimationVfx>
    {
        [SerializeField] private Animator _animator;
        private AnimatorEventsDispatcher _dispatcher;

        protected override void OnSpawned()
        {
            _dispatcher = _animator.GetBehaviour<AnimatorEventsDispatcher>();
            if (!_dispatcher)
                throw new InvalidOperationException("AnimationVfx should contain events dispatcher");

            gameObject.SetActive(true);
            _dispatcher.OnAnimationExit += OnStateExit;
        }

        protected override void OnDespawned()
        {
            if (this)
                gameObject.SetActive(false);
            
            _dispatcher.OnAnimationExit -= OnStateExit;
        }

        private void OnStateExit(AnimatorStateInfo state)
        {
            Dispose();
        }
    }
}