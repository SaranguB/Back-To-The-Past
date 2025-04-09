using Main;
using System;
using TimeSwitching;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        private Rigidbody2D playerRB;
        private TimeSwitchUIController timeSwitchUIController;
        private Animator playerAnimator;

        public PlayerController(PlayerView playerView, PlayerSO playerS0)
        {
            this.playerView = playerView;
            playerModel = new PlayerModel(playerS0);

            this.playerView.SetController(this);
        }
        public void HandleMovement()
        {
            if (!playerModel.isTimeSwitching)
            {
                Move(playerModel.horizontalInput);

                if (playerModel.isJumping && playerView.ISGrounded())
                {
                    Jump();
                    SetISJumping(false);
                }
                HandleFalling();
            }
        }
        public void Move(float horizontalInput)
        {
            float speed = horizontalInput * playerModel.movementSpeed;
            Vector2 currentvelocity = playerRB.linearVelocity;
            playerRB.linearVelocity = new Vector2(speed, currentvelocity.y);

            playerView.FlipOnDirection(horizontalInput);
        }

        public void Jump()
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocityX, playerModel.jumpForce);
        }

        public bool IsGrounded(Transform[] groundCheckPoint, float groundCheckDistance, LayerMask groundLayer)
        {
            foreach (Transform point in groundCheckPoint)
            {
                if (Physics2D.OverlapCircle(point.position, groundCheckDistance, groundLayer))
                    return true;
            }
            return false;
        }

        public void HandleFalling()
        {
            if (playerRB.linearVelocity.y < 0)
            {
                SetAnimatorBool("IsJumping", false);
                playerRB.gravityScale = playerModel.fallingSpeed;
            }
            else if (playerRB.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                playerRB.gravityScale = playerModel.fallingSpeed;
            }
            else
            {
                playerRB.gravityScale = 1f;
            }
        }

        public void SwitchTime()
        {
            GameManager.Instance.eventService.onTimeSwitched.InvokeEvent();
        }

        public void SetTimeSwitchUI(TimeSwitchUIController timeSwitchUIController)
        {
            this.timeSwitchUIController = timeSwitchUIController;
        }

        public void SetTimeSwitchSlider(bool isKeyHeld, float timeRequiredForSwitching)
        {
            if (timeSwitchUIController == null)
                Debug.Log("Null");

            timeSwitchUIController.UpdateTimeSwitchUISlider(isKeyHeld, timeRequiredForSwitching);
        }

        public void CancelTimeSwitching(float deltaTime, Animator playerAnimator)
        {
            StopPlayerMovement(playerAnimator);

            playerModel.timeSwitchingDuration += Time.deltaTime;
            SetTimeSwitchSlider(true, playerModel.timeRequiredForSwitching);

            if (playerModel.timeSwitchingDuration >= playerModel.timeRequiredForSwitching)
            {
                Debug.Log("Time Switched");
                SwitchTime();
                playerModel.isTimeSwitching = true;
            }
        }

        public void StopPlayerMovement(Animator playerAnimator)
        {
            playerModel.horizontalInput = 0f;
            playerModel.isJumping = false;
            SetAnimatorFloatValue("Speed", playerAnimator, 0f);
        }

        public void handleTimeSwitching()
        {
            SetTimeSwitchSlider(false, playerModel.timeRequiredForSwitching);
            playerModel.timeSwitchingDuration = 0f;
            playerModel.isTimeSwitching = false;
        }

        public void SetISJumping(bool value)
        {
            playerModel.isJumping = value;
        }

        public bool GetIsJumping()
            => playerModel.isJumping;

        public bool GetIsTimeSwitching()
            => playerModel.isTimeSwitching;

        public void SetAnimatorFloatValue(string parameterName, Animator playerAnimator, float value)
        {
            playerAnimator.SetFloat(parameterName, MathF.Abs(value));
        }

        internal void SetMoveInputValues(Animator playerAnimator, float horizontalInput)
        {
            playerModel.horizontalInput = horizontalInput;
            SetAnimatorFloatValue("Speed", playerAnimator, playerModel.horizontalInput);
        }

        public void SetPlayerValues(Animator playerAnimator, Rigidbody2D playerRB)
        {
            this.playerAnimator = playerAnimator;
            this.playerRB = playerRB;
        }

        public void SetAnimatorBool(string stringValue, bool boolValue)
        {
            playerAnimator.SetBool(stringValue, boolValue);
        }
    }
}
