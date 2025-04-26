using StateMachine;
using UnityEngine;

namespace Player
{
    public class AliveState : IState<PlayerController>
    {
        private Animator playerAnimator;
        public PlayerController owner { get; set; }

        public AliveState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public void OnStateEnter()
        {
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
