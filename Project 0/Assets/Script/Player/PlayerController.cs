using Audio;
using Main;
using Player.UI;
using StateMachine;
using System;
using UI;
using UnityEngine;
using Wepons.Bomb;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        private TimeSwitchUIController timeSwitchUIController;
        private PlayerUIController playerUIController;

        private BombPool bombPool;
        private PlayerStateMachine playerStateMachine;
        private HealthUIController healthUIController;

        public PlayerController(PlayerView playerView, PlayerSO playerS0, BombPool bombPool)
        {
            this.playerView = playerView;
            playerModel = new PlayerModel(playerS0);
            this.playerView.SetController(this);
            this.bombPool = bombPool;

            CreatePlayerStateMachine();
            ChangePlayerState(PlayerState.Alive);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.eventService.OnPlayerGotDamaged.AddListener(TakeDamage);
        }

        public void UnSubcribeToEvents()
        {
            GameManager.Instance.eventService.OnPlayerGotDamaged.RemoveListener(TakeDamage);
        }

        public void ChangePlayerState(PlayerState state)
            => playerStateMachine.ChangeState(state);

        private void CreatePlayerStateMachine()
        => playerStateMachine = new PlayerStateMachine(this, playerView.playerAnimator);

        public void HandleInput()
        {
            ConfigureMoveInput();
            ConfigureJumpInput();
            ConfigureTimeSwitchInput();
            ConfigureBombDeployInput();
            ConfigureTransitionToNextLevelInput();
        }

        private void ConfigureMoveInput()
        {
            playerModel.horizontalInput = Input.GetAxis("Horizontal");
            SetAnimatorFloatValue("Speed", Mathf.Abs(playerModel.horizontalInput));
        }

        public void HandleMovement()
        {
            HandleHorizontalMovement();
            HandleJumping();
            HandleFalling();
            UpdateAirDashStatus();
        }

        private void HandleHorizontalMovement()
        {
            float speed = playerModel.horizontalInput * playerModel.movementSpeed;
            Vector2 velocity = playerView.playerRB.linearVelocity;
            playerView.playerRB.linearVelocity = new Vector2(speed, velocity.y);

            if (playerModel.horizontalInput != 0)
                playerView.FlipOnDirection(playerModel.horizontalInput);
        }

        private void HandleJumping()
        {
            if (playerModel.isJumping && IsGrounded())
            {
                Jump();
                SetIsJumping(false);
            }
        }

        private void UpdateAirDashStatus()
        {
            if (IsGrounded())
                playerModel.canPlayerAirDash = true;
        }

        private void ConfigureJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SetIsJumping(true);
        }

        public void Jump()
        {
            SetAnimatorBool("IsJumping", true);
            Vector2 velocity = playerView.playerRB.linearVelocity;
            playerView.playerRB.linearVelocity = new Vector2(velocity.x, playerModel.jumpForce);
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
            float verticalVelocity = playerView.playerRB.linearVelocity.y;

            if (verticalVelocity < 0)
            {
                SetAnimatorBool("IsJumping", false);
                SetGravity(playerModel.fallingSpeed);
            }
            else if (verticalVelocity > 0 && !Input.GetKey(KeyCode.Space))
                SetGravity(playerModel.fallingSpeed);
            else
                SetGravity(1f);
        }

        private void SetGravity(float gravityScale)
           => playerView.playerRB.gravityScale = gravityScale;


        public void SetIsJumping(bool value)
            => playerModel.isJumping = value;


        public void HandleDashing()
        {
            UpdateDashInputDirection();

            if (CanStartDash())
            {
                if (IsGrounded() && playerModel.currentDashes > 0)
                {
                    GroundDash();
                }
                else if (!IsGrounded() && playerModel.canPlayerAirDash)
                {
                    AirDash();
                }
            }
            if (playerModel.isDashing)
                UpdateDashTimer();
        }

        private void UpdateDashInputDirection()
        {
            playerModel.inputDirection = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;
        }

        private bool CanStartDash()
        {
            return !playerModel.isDashing && Input.GetKeyDown(KeyCode.LeftShift);
        }

        private void UpdateDashTimer()
        {
            playerModel.dashTimer -= Time.deltaTime;

            if (playerModel.dashTimer <= 0f)
                EndDashing();
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
            playerView.playerAnimator.SetBool("IsDashing", true);
            GameManager.Instance.soundService.PlaySoundEffects(SoundType.PlayerDashing);
            playerModel.isDashing = true;
            playerModel.dashTimer = playerModel.dashDuration;
        }

        private void EndDashing()
        {
            playerView.playerAnimator.SetBool("IsDashing", false);
            playerModel.isDashing = false;

            playerView.playerRB.linearVelocity = new Vector2(
                playerModel.horizontalInput * playerModel.movementSpeed, 0);
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

                playerView.playerRB.linearVelocity = playerModel.inputDirection * playerModel.dashSpeed;
            }
        }

        private void ConfigureTransitionToNextLevelInput()
        {
            if (Input.GetKeyDown(KeyCode.F) && playerModel.isPlayerHasKey && playerModel.canUnlockDoor)
                UnlockDoor();
        }

        private void UnlockDoor()
        {
            playerModel.canUnlockDoor = false;
            GameManager.Instance.eventService.OnPlayerOpenedDoor.InvokeEvent();
            playerView.LevelFinished();
        }

        public void LevelFinished()
           => GameManager.Instance.eventService.OnPlayerFinishedLevel.InvokeEvent();

        private void ConfigureBombDeployInput()
        {
            if (!playerModel.canDeployBomb)
                return;

            HandleBombInput();
        }

        private void HandleBombInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
                StartChargingBomb();

            if (Input.GetKey(KeyCode.LeftControl))
                ChargeBomb();

            if (Input.GetKeyUp(KeyCode.LeftControl))
                ReleaseBomb();
        }

        private void StartChargingBomb()
        {
            playerUIController.EnableBombThrowChargingBar(true);
            playerModel.isHoldingBombKey = true;
            playerModel.bombHoldTimer = 0f;
        }

        private void ChargeBomb()
        {
            DisplayBombThrowIndicator(true);
            playerModel.bombHoldTimer += Time.deltaTime;
        }

        private void ReleaseBomb()
        {
            if (playerModel.bombHoldTimer >= playerModel.bombThreshold)
                ThrowBomb();
            else
                DeployBomb();

            playerUIController.ResetUI();
        }

        private void DisplayBombThrowIndicator(bool value)
           => playerUIController.UpdateBombThrowUISlider(value, playerModel.bombThreshold);

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
            bool isTabHeld = Input.GetKey(KeyCode.Tab);

            if (isTabHeld)
            {
                if (!playerModel.isTimeSwitching)
                    BeginTimeSwitching(Time.deltaTime);
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
                CancelTimeSwitching();

            GameManager.Instance.cameraController.UpdateCameraShake(isTabHeld, playerView.playerImpulseSource);
        }

        private void BeginTimeSwitching(float deltaTime)
        {
            SetAnimatorBool("IsTimeSwitching", true);

            PlayTimeSwitchFeedback();
            StopPlayerActions();

            playerModel.timeSwitchingDuration += deltaTime;
            SetTimeSwitchSlider(true, playerModel.timeRequiredForSwitching);

            if (playerModel.timeSwitchingDuration >= playerModel.timeRequiredForSwitching)
                ExecuteTimeSwitch();
        }

        private void PlayTimeSwitchFeedback()
        {

            if (!GameManager.Instance.soundService.IsAudioEffectsPlaying())
                GameManager.Instance.soundService.PlaySoundEffects(SoundType.TimeSwitchingSound);

            if (!playerView.timeSwitchParticle.isPlaying)
                playerView.timeSwitchParticle.Play();
        }

        private void StopPlayerActions()
        {
            playerModel.horizontalInput = 0f;
            SetAnimatorFloatValue("Speed", 0f);
            playerModel.isJumping = false;
            playerModel.canDeployBomb = false;
        }

        private void CancelTimeSwitching()
        {
            GameManager.Instance.soundService.StopPlayingSoundEffect();

            playerView.timeSwitchParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            SetAnimatorBool("IsTimeSwitching", false);
            SetTimeSwitchSlider(false, playerModel.timeRequiredForSwitching);

            playerModel.timeSwitchingDuration = 0f;
            playerModel.isTimeSwitching = false;
            playerModel.canDeployBomb = true;
        }

        private void ExecuteTimeSwitch()
        {
            playerModel.isTimeSwitching = true;
            SetAnimatorBool("IsTimeSwitching", false);
            playerView.timeSwitchParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            GameManager.Instance.eventService.onTimeSwitched.InvokeEvent();
        }

        private void SetTimeSwitchSlider(bool show, float timeRequired)
            => timeSwitchUIController.UpdateTimeSwitchUISlider(show, timeRequired);

        public void SetTimeSwitchUI(TimeSwitchUIController timeSwitchUIController)
            => this.timeSwitchUIController = timeSwitchUIController;

        public void SetAnimatorFloatValue(string parameterName, float value)
            => playerView.playerAnimator.SetFloat(parameterName, MathF.Abs(value));

        public void SetAnimatorBool(string stringValue, bool boolValue)
            => playerView.playerAnimator.SetBool(stringValue, boolValue);

        public void OnPlayerPositionChanged(Vector2 position)
            => GameManager.Instance.eventService.onPlayerPositionChanged.InvokeEvent(position);

        public void SetPlayerUI(PlayerUIController playerUIController)
            => this.playerUIController = playerUIController;

        public bool IsPlayerHasKey()
             => playerModel.isPlayerHasKey;

        public void OnKeyCollected()
        {
            playerModel.isPlayerHasKey = true;
            GameManager.Instance.eventService.OnPlayerGotKey.InvokeEvent();
        }

        public void IsInfrontOfFinalDoor(bool value)
            => playerModel.canUnlockDoor = value;

        public void SetHealthUI(HealthUIController healthUIController)
        {
            this.healthUIController = healthUIController;
            healthUIController.SetNumberOfLives(playerModel.numberOfLives);
        }

        public void TakeDamage(int damage)
        {
            playerModel.currentLives -= damage;
            healthUIController.RemoveLives(damage);

            if (GetCurrentPlayerState() is not DeadState)
                playerStateMachine.ChangeState(PlayerState.Hurt);

            if (playerModel.currentLives <= 0)
                playerStateMachine.ChangeState(PlayerState.Dead);
        }

        public IState<PlayerController> GetCurrentPlayerState()
            => playerStateMachine.GetCurrentState();

        public void OnPlayerDestroyed()
            => GameManager.Instance.eventService.OnPlayerDead.InvokeEvent();

        public void OnPlayerDead()
        {
            GameManager.Instance.soundService.StopBackgroundSong();
            GameManager.Instance.soundService.PlaySoundEffects(SoundType.LevelLostSound);
            GameManager.Instance.eventService.OnPlayerDeadWithParams.InvokeEvent(PlayerState.Dead);
        }
    }
}
