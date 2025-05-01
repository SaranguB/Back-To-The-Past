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
        public PlayerView PlayerView => playerView;

        private PlayerModel playerModel;
        public PlayerModel PlayerModel => playerModel;

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
            ChangePlayerState(PlayerState.Idle);
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

        public void UpdateState()
        {
            playerStateMachine.Update();
        }

        public void FixedUpdateState()
        {
            playerStateMachine.FixedUpdate();
        }

        public void ChangePlayerState(PlayerState newState)
        {
            playerStateMachine.ChangeState(newState);
        }

        private void CreatePlayerStateMachine()
        => playerStateMachine = new PlayerStateMachine(this, playerView.playerAnimator);

        public void HandleInput()
        {
            ConfigureTransitionToNextLevelInput();
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

        public PlayerUIController GetPlayerUIController()
            => playerUIController;

        public BombPool GetBombPool()
            => bombPool;

        public void SetTimeSwitchSlider(bool show, float timeRequired)
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

            if (playerModel.currentLives <= 0)
                playerStateMachine.ChangeState(PlayerState.Dead);

            if (GetCurrentPlayerState() is not DeadState)
                PlayerView.playerAnimator.SetBool("IsHurt", true);
        }

        public void DisableHurtAnimation()
           => playerView.DisableHurtAnimation();

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
