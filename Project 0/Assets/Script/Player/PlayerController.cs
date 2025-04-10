using Main;
using System;
using TimeSwitching;
using UI;
using UnityEngine;
using Wepons.Bomb;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        private Rigidbody2D playerRB;
        private TimeSwitchUIController timeSwitchUIController;
        private Animator playerAnimator;
        private BombPool bombPool;
        private PlayerStateMachine playerStateMachine;
        public PlayerController(PlayerView playerView, PlayerSO playerS0, BombPool bombPool)
        {
            this.playerView = playerView;
            playerModel = new PlayerModel(playerS0);
            this.playerView.SetController(this);

            CreatePlayerStateMachine();
            ChangePlayerState(PlayerState.Alive);
            this.bombPool = bombPool;
        }

        private void ChangePlayerState(PlayerState state)
        {
            playerStateMachine.ChangeState(state);
        }

        private void CreatePlayerStateMachine()
        {
          playerStateMachine = new PlayerStateMachine(this);
        }

        public void HandleInput()
        {
            SetMoveInput();
            SetJumpInput();
            SetTimeSwitchInput();
            SetBombDeployInput();
        }

        private void SetBombDeployInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerModel.canDeployBomb)
            {
                DeployBomb();
            }

        }

        private void DeployBomb()
        {
            BombController bombToDeploy = bombPool.GetBomb();
            bombToDeploy.ConfigureBomb(playerView.bombBagPosition);
        }

        private void SetTimeSwitchInput()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                if (!playerModel.isTimeSwitching)
                {
                    HandleTimeSwitching(Time.deltaTime);
                }
                else
                {
                    Debug.Log("Yes");
                    //StopPlayerActions();
                }
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                CancelTimeSwitching();
            }
        }

        public void HandleTimeSwitching(float deltaTime)
        {
            SetAnimatorBool("IsTimeSwitching", true);
            StopPlayerActions();

            playerModel.timeSwitchingDuration += Time.deltaTime;
            SetTimeSwitchSlider(true, playerModel.timeRequiredForSwitching);

            if (playerModel.timeSwitchingDuration >= playerModel.timeRequiredForSwitching)
            {
                Debug.Log("Time Switched");
                SwitchTime();
                playerModel.isTimeSwitching = true;
            }
        }

        public void CancelTimeSwitching()
        {
            SetAnimatorBool("IsTimeSwitching", false);
            SetTimeSwitchSlider(false, playerModel.timeRequiredForSwitching);
            playerModel.timeSwitchingDuration = 0f;
            playerModel.isTimeSwitching = false;
            playerModel.canDeployBomb = true;
        }

        public void SwitchTime()
        {
            SetAnimatorBool("IsTimeSwitching", false);
            GameManager.Instance.eventService.onTimeSwitched.InvokeEvent();
        }

        public void SetTimeSwitchUI(TimeSwitchUIController timeSwitchUIController)
        {
            this.timeSwitchUIController = timeSwitchUIController;
        }

        public void SetTimeSwitchSlider(bool isKeyHeld, float timeRequiredForSwitching)
        {
            timeSwitchUIController.UpdateTimeSwitchUISlider(isKeyHeld, timeRequiredForSwitching);
        }

        private void SetMoveInput()
        {
            playerModel.horizontalInput = Input.GetAxis("Horizontal");
            SetAnimatorFloatValue("Speed", playerModel.horizontalInput);
        }

        public void HandleMovement()
        {
            Move(playerModel.horizontalInput);

            if (playerModel.isJumping && IsGrounded())
            {
                Jump();
                SetISJumping(false);
            }
            HandleFalling();
        }

        public void Move(float horizontalInput)
        {
            float speed = horizontalInput * playerModel.movementSpeed;
            Vector2 currentvelocity = playerRB.linearVelocity;
            playerRB.linearVelocity = new Vector2(speed, currentvelocity.y);

            if (horizontalInput != 0)
                playerView.FlipOnDirection(horizontalInput);
        }

        private void SetJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetISJumping(true);

            }
        }

        public void Jump()
        {
            SetAnimatorBool("IsJumping", true);
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocityX, playerModel.jumpForce);
        }

        public bool IsGrounded()
        {
            foreach (Transform point in playerView.groundCheckPoint)
            {
                if (Physics2D.OverlapCircle(point.position, playerModel.groundCheckDistance, playerView.groundLayer))
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

        public void StopPlayerActions()
        {
            playerModel.horizontalInput = 0f;
            playerModel.isJumping = false;
            SetAnimatorFloatValue("Speed", 0f);
            playerModel.canDeployBomb = false;
        }


        public void SetISJumping(bool value)
        {
            playerModel.isJumping = value;
        }

        public void SetAnimatorFloatValue(string parameterName, float value)
        {
            playerAnimator.SetFloat(parameterName, MathF.Abs(value));
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
