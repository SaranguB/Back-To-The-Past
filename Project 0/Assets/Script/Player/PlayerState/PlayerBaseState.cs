using Player.UI;
using StateMachine;
using UnityEngine;

namespace Player
{
    public abstract class PlayerBaseState : IState<PlayerController>
    {
        public PlayerController owner { get; set; }

        protected bool HandleMovementInput()
        {
            if (Mathf.Abs(owner.PlayerModel.horizontalInput) > 0.01f)
            {
                owner.ChangePlayerState(PlayerState.Running);
                return true;
            }
            return false;
        }

        protected bool HandleJumpInput()
        {
            if (owner.PlayerModel.isJumping && IsGrounded())
            {
                owner.ChangePlayerState(PlayerState.Jumping);
                return true;
            }
            return false;
        }

        protected bool HandleDashInput()
        {
            if (CanStartDash())
            {
                owner.ChangePlayerState(PlayerState.Dashing);
                return true;
            }
            return false;
        }

        protected bool HandleFallingCheck()
        {
            if (owner.PlayerView.playerRB.linearVelocity.y < 0 && !IsGrounded())
            {
                owner.ChangePlayerState(PlayerState.Falling);
                return true;
            }
            return false;
        }

        protected bool HandleTimeSwitchInput()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                owner.ChangePlayerState(PlayerState.TimeSwitching);
                return true;
            }
            return false;
        }

        protected bool HandleAttackInput()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                owner.ChangePlayerState(PlayerState.Attack);
                return true;
            }
            return false;
        }

        protected void DisableHurtAnimation()
            => owner.DisableHurtAnimation();

        protected void ConfigureMoveInput()
        {
            owner.PlayerModel.horizontalInput = Input.GetAxis("Horizontal");
        }

        protected void HandleHorizontalMovement()
        {
            float speed = owner.PlayerModel.horizontalInput * owner.PlayerModel.movementSpeed;
            Vector2 velocity = owner.PlayerView.playerRB.linearVelocity;
            owner.PlayerView.playerRB.linearVelocity = new Vector2(speed, velocity.y);

            if (owner.PlayerModel.horizontalInput != 0)
                owner.PlayerView.FlipOnDirection(owner.PlayerModel.horizontalInput);
        }

        protected void StopHorizontalMovement()
        {
            var velocity = owner.PlayerView.playerRB.linearVelocity;
            owner.PlayerView.playerRB.linearVelocity = new Vector2(0, velocity.y);
            owner.PlayerModel.horizontalInput = 0f;
        }

        protected void ConfigureJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SetIsJumping(true);
        }

        protected bool IsGrounded()
        {
            foreach (Transform point in owner.PlayerView.groundCheckPoint)
            {
                if (Physics2D.OverlapCircle(point.position, owner.PlayerModel.groundCheckDistance, owner.PlayerView.groundLayer))
                    return true;
            }
            return false;
        }

        protected void SetGravity(float gravityScale)
           => owner.PlayerView.playerRB.gravityScale = gravityScale;

        protected void SetIsJumping(bool value)
            => owner.PlayerModel.isJumping = value;

        protected bool CanStartDash()
        {
            return !owner.PlayerModel.isDashing && Input.GetKeyDown(KeyCode.LeftShift);
        }

        public abstract void OnStateEnter();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void OnStateExit();
    }
}
