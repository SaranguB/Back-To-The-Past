using StateMachine;
using UnityEngine;

namespace Player
{ 
    public class DeadState : PlayerBaseState
    {
        private Animator playerAnimator;

        public DeadState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
            DisableHurtAnimation();
            playerAnimator.SetBool("IsDead", true);
        }

        public override void UpdateState()
        {
        }

        public override void FixedUpdateState()
        {
        }

        public override void OnStateExit()
        {
        }
    }
}
