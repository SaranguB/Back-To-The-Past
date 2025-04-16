using Main;
using Player.UI;
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
        private PlayerUIController playerUIController;
        private Animator playerAnimator;
        private BombPool bombPool;
        private PlayerStateMachine playerStateMachine;
        private HealthUIController healthUIController;

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
            ConfigureMoveInput();
            ConfigureJumpInput();
            ConfigureTimeSwitchInput();
            ConfigureBombDeployInput();
            ConfigureTransitionToNextLevelInput();
        }

        private void ConfigureTransitionToNextLevelInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && playerModel.isPlayerHasKey && playerModel.canUnlockDoor)
            {
                UnlockDoor();
            }
        }

        private void UnlockDoor()
        {
           playerModel.canUnlockDoor = false;
        }

        private void ConfigureBombDeployInput()
        {

            if (playerModel.canDeployBomb)
            {

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    playerUIController.EnableBombThrowChargingBar(true);
                    playerModel.isHoldingBombKey = true;
                    playerModel.bombHoldTimer = 0f;
                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    DisplayBombThrowIndicator(true);
                    playerModel.bombHoldTimer += Time.deltaTime;
                }

                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    if (playerModel.bombHoldTimer >= playerModel.bombThreshold)
                    {
                        ThrowBomb();
                    }
                    else
                    {
                        DeployBomb();
                    }
                    playerUIController.ResetUI();

                }
            }
        }

        private void DisplayBombThrowIndicator(bool value)
        {
            playerUIController.UpdateBombThrowUISlider(value, playerModel.bombThreshold);
        }

        private void ThrowBomb()
        {
            BombController bombToDeploy = CreateBomb();

            Vector2 throwDirection = playerView.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            bombToDeploy.LaunchBomb(throwDirection, playerModel.bombThrowForceX, playerModel.bombThrowForceY);
        }

        private void DeployBomb()
        {
            CreateBomb();
        }

        private BombController CreateBomb()
        {
            BombController bombToDeploy = bombPool.GetBomb();
            bombToDeploy.ConfigureBomb(playerView.bombBagPosition);
            return bombToDeploy;
        }

        private void ConfigureTimeSwitchInput()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                if (!playerModel.isTimeSwitching)
                {
                    HandleTimeSwitching(Time.deltaTime);
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

        private void ConfigureMoveInput()
        {
            playerModel.horizontalInput = Input.GetAxis("Horizontal");
            SetAnimatorFloatValue("Speed", playerModel.horizontalInput);
        }

        public void HandleMovement()
        {
            Move(playerModel.horizontalInput);

            if (IsGrounded())
            {
                playerModel.canPlayerAirDash = true;
            }

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

        private void ConfigureJumpInput()
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

        public void OnPlayerPositionChanged(Vector2 position)
        {
            GameManager.Instance.eventService.onPlayerPositionChanged.InvokeEvent(position);
        }

        public void HandleDashing()
        {
            playerModel.inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (!playerModel.isDashing && Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (playerModel.currentDashes > 0 && IsGrounded())
                {
                    GroundDash();
                }
                else if (!IsGrounded() && playerModel.canPlayerAirDash)
                {
                    AirDash();
                }
            }

            if (playerModel.isDashing)
            {
                playerModel.dashTimer -= Time.deltaTime;

                if (playerModel.dashTimer < 0)
                    EndDashing();
            }

        }

        private void AirDash()
        {
            StartDashing();
            playerModel.canPlayerAirDash = false;
        }

        private void GroundDash()
        {
            StartDashing();
            playerModel.currentDashes--;
            playerModel.canPlayerAirDash = true;
        }

        private void StartDashing()
        {
            playerAnimator.SetBool("IsDashing", true);
            playerModel.isDashing = true;
            playerModel.dashTimer = playerModel.dashDuration;
        }

        private void EndDashing()
        {
            playerAnimator.SetBool("IsDashing", false);
            playerModel.isDashing = false;

            playerRB.linearVelocity = new Vector2(playerModel.horizontalInput * playerModel.movementSpeed, 0);
        }

        public void ExecuteDashing()
        {
            if (playerModel.isDashing)
            {
                if (playerModel.inputDirection == Vector2.zero)
                {
                    float facingDirection = Mathf.Sign(playerView.transform.localScale.x);
                    playerModel.inputDirection = new Vector2(facingDirection, 0);
                }

                playerRB.linearVelocity = playerModel.inputDirection * playerModel.dashSpeed;
            }
        }

        public void SetPlayerUI(PlayerUIController playerUIController)
        {
            this.playerUIController = playerUIController;
        }

        public bool IsPlayerHasKey()
             => playerModel.isPlayerHasKey;

        public void OnKeyCollected()
        {
            playerModel.isPlayerHasKey = true;
        }

        public void IsInfrontOfFinalDoor(bool value)
        {
            playerModel.canUnlockDoor = value;
        }

        public void SetHealthUI(HealthUIController healthUIController)
        {
            this.healthUIController = healthUIController;
            healthUIController.SetNumberOfLives(playerModel.numberOfLives);
        }

        public void TakeDamageFromBomb(int damage)
        {
            
            healthUIController.RemoveLives(damage);
        }
    }
}
