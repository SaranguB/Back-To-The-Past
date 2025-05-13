using Player.UI;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class JumpingState : PlayerBaseState
    {
        private Animator playerAnimator;
       
        public JumpingState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
            DisableHurtAnimation();
            ApplyJumpForce();
            SetIsJumping(false);
        }

        private void ApplyJumpForce()
        {
            owner.SetAnimatorBool("IsJumping", true);
            var velocity = owner.PlayerView.playerRB.linearVelocity;
            owner.PlayerView.playerRB.linearVelocity = new Vector2(velocity.x, owner.PlayerModel.jumpForce);
        }

        public override void UpdateState()
        {
            ConfigureMoveInput();

            if (HandleDashInput()) return;
            if (HandleAttackInput()) return;

            UpdateAirDashStatus();
        }

        public override void FixedUpdateState()
        {
            HandleHorizontalMovement();
            HandleEarlyJumpRelease();
            HandleFallingTransition();
        }

        private void HandleEarlyJumpRelease()
        {
            float verticalVelocity = owner.PlayerView.playerRB.linearVelocity.y;
            if (verticalVelocity > 0 && !Input.GetKey(KeyCode.Space))
            {
                owner.PlayerView.playerRB.linearVelocity = new Vector2(
                    owner.PlayerView.playerRB.linearVelocity.x,
                    verticalVelocity * 0.5f
                );
            }
        }

        private void HandleFallingTransition()
        {
            float verticalVelocity = owner.PlayerView.playerRB.linearVelocity.y;

            if (verticalVelocity < 0)
            {
                if (IsGrounded())
                {
                    owner.ChangePlayerState(PlayerState.Idle);
                }
                else
                {
                    owner.ChangePlayerState(PlayerState.Falling);
                }
            }
        }

        public override void OnStateExit()
        {
            owner.SetAnimatorBool("IsJumping", false);
        }
    }
}
