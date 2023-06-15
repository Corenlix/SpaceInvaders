using System;
using UnityEngine;

namespace Code
{
    public class AnimatorEventsDispatcher : StateMachineBehaviour
    {
        public event Action<AnimatorStateInfo> OnAnimationEnter;
        public event Action<AnimatorStateInfo> OnAnimationExit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnAnimationEnter?.Invoke(stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnAnimationExit?.Invoke(stateInfo);
        }
    }
}
