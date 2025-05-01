using Player.UI;
using StateMachine;
using UnityEngine;
using Wepons.Bomb;

namespace Player
{
    public class AttackingState : PlayerBaseState
    {
        private Animator playerAnimator;

        public AttackingState(Animator playerAnimator)
        {
            this.playerAnimator = playerAnimator;
        }

        public override void OnStateEnter()
        {
        }

        public override void UpdateState()
        {
            ConfigureMoveInput();
            HandleBombInput();

            if (HandleDashInput()) return;
            if (HandleJumpInput()) return;
            if (HandleIdleTransition()) return;

        }

        protected bool HandleIdleTransition()
        {
            if (!Input.GetKey(KeyCode.LeftControl) && !owner.PlayerModel.isHoldingBombKey)
            {
                owner.ChangePlayerState(PlayerState.Idle);
                return true;
            }
            return false;
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
            owner.GetPlayerUIController().EnableBombThrowChargingBar(true);
            owner.PlayerModel.isHoldingBombKey = true;
            owner.PlayerModel.bombHoldTimer = 0f;
        }

        private void ChargeBomb()
        {
            owner.GetPlayerUIController().EnableBombThrowChargingBar(true);
            DisplayBombThrowIndicator(true);
            owner.PlayerModel.bombHoldTimer += Time.deltaTime;
        }

        private void DisplayBombThrowIndicator(bool value)
        {
            owner.GetPlayerUIController().UpdateBombThrowUISlider(value, owner.PlayerModel.bombThreshold);
        }

        private void ReleaseBomb()
        {
            if (owner.PlayerModel.bombHoldTimer >= owner.PlayerModel.bombThreshold)
                ThrowBomb();
            else
                DeployBomb();
        }

        private void ThrowBomb()
        {
            BombController bomb = CreateBomb();
            Vector2 direction = owner.PlayerView.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            bomb.LaunchBomb(direction, owner.PlayerModel.bombThrowForceX, owner.PlayerModel.bombThrowForceY);
        }

        private void DeployBomb()
        {
            CreateBomb();
        }

        private BombController CreateBomb()
        {
            Debug.Log("Yes");
            BombController bomb = owner.GetBombPool().GetBomb();
            bomb.ConfigureBomb(owner.PlayerView.bombBagPosition);
            return bomb;
        }

        public override void FixedUpdateState()
        {
            HandleHorizontalMovement();
        }

        public override void OnStateExit()
        {
            owner.PlayerModel.isHoldingBombKey = false;
            owner.PlayerModel.bombHoldTimer = 0f;
            owner.GetPlayerUIController().ResetUI();
        }
    }
}
