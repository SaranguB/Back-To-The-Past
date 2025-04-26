using StateMachine;
using UnityEngine;

namespace Player
{ 
    public class DeadState : IState<PlayerController>
    {
        private Animator playerAnimator;

        public DeadState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public PlayerController owner { get; set; }

        public void OnStateEnter()
        {
            playerAnimator.SetBool("IsDead", true);
        }

        public void UpdateState()
        {
        }

        public void FixedUpdateState()
        {
        }

        public void OnStateExit()
        {
        }
    }
}
