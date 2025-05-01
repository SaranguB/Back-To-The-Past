using StateMachine;
using UnityEngine;

namespace Player
{
    public class FallingState : PlayerBaseState
    {
        private Animator playerAnimator;

        public FallingState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
            DisableHurtAnimation();
            owner.SetAnimatorBool("IsJumping", false);
        }

        public override void UpdateState()
        {
            ConfigureMoveInput();

            if (HandleDashInput()) return;
            if (HandleAttackInput()) return;
            if (IsGrounded())
            {
                owner.ChangePlayerState(PlayerState.Idle);
            }
        }

        public override void FixedUpdateState()
        {
            HandleHorizontalMovement();
            ApplyFallingPhysics();
        }

        private void ApplyFallingPhysics()
        {
            float verticalVelocity = owner.PlayerView.playerRB.linearVelocity.y;

            if (verticalVelocity < 0)
            {
                SetGravity(owner.PlayerModel.fallingSpeed);
            }
            else if (verticalVelocity > 0 && !Input.GetKey(KeyCode.Space))
            {
                owner.PlayerView.playerRB.linearVelocity = new Vector2(
                    owner.PlayerView.playerRB.linearVelocity.x,
                    verticalVelocity * 0.5f
                );
                SetGravity(owner.PlayerModel.fallingSpeed);
            }
            else
            {
                SetGravity(1f);
            }
        }

        public override void OnStateExit()
        {
            SetGravity(1f);
        }
    }
}
