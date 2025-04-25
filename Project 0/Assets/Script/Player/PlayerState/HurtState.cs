using StateMachine;
using UnityEngine;

namespace Player
{
    public class HurtState : IState<PlayerController>
    {
        public PlayerController owner { get; set; }

        private Animator playerAnimator;

        public HurtState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public void OnStateEnter()
        {

            playerAnimator.SetBool("IsHurt", true);
        }

        public void UpdateState()
        {
        }

        public void FixedUpdateState()
        {

        }

        public void OnStateExit()
        {
            playerAnimator.SetBool("IsHurt", false);
        }


    }
}
