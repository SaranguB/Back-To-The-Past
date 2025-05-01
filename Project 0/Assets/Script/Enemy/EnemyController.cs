using Main;
using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyController
    {
        protected Vector2 playerPosition;
        protected PlayerState playerState;

        public EnemyController()
        {
            playerState = PlayerState.Idle;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.eventService.onPlayerPositionChanged.AddListener(SetPlayerPosition);
            GameManager.Instance.eventService.OnPlayerDeadWithParams.AddListener(SetPlayerState);
        }

        public virtual void FacePlayer()
        {
            Vector2 direction = playerPosition - (Vector2)GetEnemyTransform().position;

            if (direction.x > 0)
                GetEnemyTransform().localScale = new Vector3(-1, 1, 1);
            else if (direction.x < 0)
                GetEnemyTransform().localScale = new Vector3(1, 1, 1);
        }

        public virtual void PlayerEnteredRange()
        {
        }

        public virtual void PlayerExitRanged()
        {
        }
        public virtual void FireBomb()
        {
        }

        public virtual void FireCanon()
        {
        }

        public virtual void Fire()
        {
        }

        public virtual void MeleAttack()
        {
        }

        public void SetPlayerPosition(Vector2 playerPosition)
             => this.playerPosition = playerPosition;

        public void SetPlayerState(PlayerState playerState)
            => this.playerState = playerState;

        public PlayerState GetPlayerState()
            => playerState;

        public abstract Transform GetEnemyTransform();
        public abstract bool IsInCastingState();
        public abstract void SetEnemyPosition(Vector2 pos);
        public abstract Vector2 GetPlayerPosition();
        public abstract float GetSpeed();
        public abstract void UpdateStateMachine();
        public abstract float GetAttackRange();
        public abstract void ChangeState(EnemyStates state);
        public abstract float GetAttackDelay();
        public abstract void FixedUpdateStateMachine();
        public abstract void TakeDamage(int damage);
        public abstract bool IsEnemyViewActiveAndEnabled();
        public abstract EnemySO GetEnemyData();
        public abstract void UnsubscribeToEvents();
    }
}
