using Audio;
using Main;
using Player.UI;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class DashingState : PlayerBaseState
    {
        public DashingState()
        {
        }

        public override void OnStateEnter()
        {
            DisableHurtAnimation();

            if (!owner.PlayerModel.isDashing)
            {
                HandleDashing();
            }
          
        }

        public override void UpdateState()
        {
            UpdateDashInputDirection();

            if (owner.PlayerModel.isDashing)
            {
                UpdateDashTimer();
            }


            if (owner.PlayerModel.dashTimer <= 0f)
            {
                EndDashing();
            }
        }

        public void UpdateDashInputDirection()
        {
            owner.PlayerModel.inputDirection = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;

            FlipSprite(owner.PlayerModel.inputDirection);
        }

        private void FlipSprite(Vector2 direction)
        {
            if (direction.x == 0) return;

            float scaleX = Mathf.Sign(direction.x);
            Vector3 currentScale = owner.PlayerView.transform.localScale;

            if (currentScale.x != scaleX)
            {
                currentScale.x = scaleX;
                owner.PlayerView.transform.localScale = currentScale;
            }
        }

        private void HandleDashing()
        {
            if (IsGrounded() && owner.PlayerModel.currentDashes > 0)
            {
                Debug.Log("ground dashing");
                GroundDash();
            }
            else if (!IsGrounded() && owner.PlayerModel.canPlayerAirDash)
            {
                Debug.Log("air dashing");
                AirDash();
            }
            else
            {
                owner.ChangePlayerState(PlayerState.Idle);
            }
        }

        private void GroundDash()
        {
            StartDashing();
            owner.PlayerModel.currentDashes--;
        }

        private void AirDash()
        {
            StartDashing();
            owner.PlayerModel.canPlayerAirDash = false;
        }

        private void StartDashing()
        {
            owner.PlayerModel.dashTimer = owner.PlayerModel.dashDuration;
            owner.PlayerView.playerAnimator.SetBool("IsDashing", true);
            GameManager.Instance.soundService.PlaySoundEffects(SoundType.PlayerDashing);
            owner.PlayerModel.isDashing = true;
        }

        private void UpdateDashTimer()
        {
            owner.PlayerModel.dashTimer -= Time.deltaTime;
        }

        private void EndDashing()
        {
            owner.ChangePlayerState(PlayerState.Idle);
        }

        public override void FixedUpdateState()
        {
            if (owner.PlayerModel.isDashing)
            {
                if (owner.PlayerModel.inputDirection == Vector2.zero)
                {
                    owner.PlayerModel.inputDirection = new Vector2(Mathf.Sign(owner.PlayerView.transform.localScale.x), 0);
                }

                owner.PlayerView.playerRB.linearVelocity = owner.PlayerModel.inputDirection * owner.PlayerModel.dashSpeed;
            }
        }

        public override void OnStateExit()
        {
            owner.PlayerModel.canPlayerAirDash = false;
            owner.PlayerView.playerAnimator.SetBool("IsDashing", false);
            owner.PlayerModel.isDashing = false;
            owner.PlayerModel.dashTimer = owner.PlayerModel.dashDuration;
            owner.PlayerView.playerRB.linearVelocity = new Vector2(owner.PlayerModel.horizontalInput * owner.PlayerModel.movementSpeed, 0);
        }
    }
}
