using Player.UI;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class RunningState : PlayerBaseState
    {
        private Animator playerAnimator;

        public RunningState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
            DisableHurtAnimation();
            owner.SetAnimatorFloatValue("Speed", 1f);
        }

        public override void UpdateState()
        {
            ConfigureMoveInput();
            ConfigureJumpInput();

            if (HandleFallingTransition()) return;
            if (HandleJumpInput()) return;
            if (HandleDashInput()) return;
            if (HandleIdleTransition()) return;
            if (HandleAttackInput()) return;
            if (HandleTimeSwitchInput()) return;
        }

        private bool HandleIdleTransition()
        {
            if (owner.PlayerModel.horizontalInput == 0)
            {
                owner.ChangePlayerState(PlayerState.Idle);
                return true;
            }
            return false;
        }

        public override void FixedUpdateState()
        {
            HandleHorizontalMovement();
        }

        public override void OnStateExit()
        {
            owner.SetAnimatorFloatValue("Speed", 0f);
            StopHorizontalMovement();
        }

        private bool HandleFallingTransition()
        {
            float verticalVelocity = owner.PlayerView.playerRB.linearVelocity.y;
            if (verticalVelocity <= 0 && !IsGrounded())
            {
                owner.ChangePlayerState(PlayerState.Falling);
                return true;
            }
            return false;
        }
    }
}
