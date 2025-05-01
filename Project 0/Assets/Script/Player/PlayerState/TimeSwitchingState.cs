using Audio;
using Main;
using Player.UI;
using StateMachine;
using UI;
using UnityEngine;

namespace Player
{
    public class TimeSwitchingState : PlayerBaseState
    {
        private Animator playerAnimator;

        public TimeSwitchingState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
            DisableHurtAnimation();
        }

        public override void UpdateState()
        {
            HandleTimeSwitch();
        }

        private void HandleTimeSwitch()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                if (!owner.PlayerModel.isTimeSwitching)
                {
                    TryStartTimeSwitching(Time.deltaTime);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                CancelTimeSwitching();
            }
        }

        private void TryStartTimeSwitching(float deltaTime)
        {
            SetSwitchingState(true);
            ApplySwitchingRestrictions();
            UpdateSwitchingTimer(deltaTime);

            if (owner.PlayerModel.timeSwitchingDuration >= owner.PlayerModel.timeRequiredForSwitching)
            {
                ExecuteTimeSwitch();
            }
        }

        private void SetSwitchingState(bool isSwitching)
        {
            owner.SetAnimatorBool("IsTimeSwitching", isSwitching);
            owner.SetTimeSwitchSlider(isSwitching, owner.PlayerModel.timeRequiredForSwitching);

            if (isSwitching)
            {
                PlaySwitchEffects();
            }
            else
            {
                StopSwitchEffects();
            }
        }

        private void PlaySwitchEffects()
        {
            if (!GameManager.Instance.soundService.IsAudioEffectsPlaying())
                GameManager.Instance.soundService.PlaySoundEffects(SoundType.TimeSwitchingSound);

            if (!owner.PlayerView.timeSwitchParticle.isPlaying)
                owner.PlayerView.timeSwitchParticle.Play();
        }

        private void StopSwitchEffects()
        {
            GameManager.Instance.soundService.StopPlayingSoundEffect();
            owner.PlayerView.timeSwitchParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void ApplySwitchingRestrictions()
        {
            owner.PlayerModel.horizontalInput = 0f;
            owner.PlayerModel.isJumping = false;
            owner.PlayerModel.canDeployBomb = false;

            owner.SetAnimatorFloatValue("Speed", 0f);
        }

        private void UpdateSwitchingTimer(float deltaTime)
        {
            owner.PlayerModel.timeSwitchingDuration += deltaTime;
        }

        private void CancelTimeSwitching()
        {
            SetSwitchingState(false);
            ResetSwitchingState();
            owner.ChangePlayerState(PlayerState.Idle);
        }

        private void ResetSwitchingState()
        {
            owner.PlayerModel.timeSwitchingDuration = 0f;
            owner.PlayerModel.isTimeSwitching = false;
            owner.PlayerModel.canDeployBomb = true;
        }

        private void ExecuteTimeSwitch()
        {
            owner.PlayerModel.isTimeSwitching = true;
            SetSwitchingState(false);
            GameManager.Instance.eventService.onTimeSwitched.InvokeEvent();
        }

        public override void FixedUpdateState() { }

        public override void OnStateExit() { }
    }
}
