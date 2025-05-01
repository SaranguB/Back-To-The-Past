using StateMachine;
using UnityEngine;

namespace Player
{
    public class IdleState : PlayerBaseState
    {
        private Animator playerAnimator;

        public IdleState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
            owner.SetAnimatorFloatValue("Speed", 0);
            StopHorizontalMovement();
        }

        public override void UpdateState()
        {
           ConfigureMoveInput();
            ConfigureJumpInput();

            if (HandleTimeSwitchInput()) return;
            if (HandleAttackInput()) return;
            if (HandleMovementInput()) return;
            if (HandleDashInput()) return;
            if (HandleJumpInput()) return;
            if (HandleFallingCheck()) return;

            StopHorizontalMovement();
        }

        public override void FixedUpdateState() { }

        public override void OnStateExit() { }
    }
}
