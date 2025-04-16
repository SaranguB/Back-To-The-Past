using Main;
using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyController
    {
        private EnemyViewCollection enemyViewCollection;

        protected Vector2 playerPosition;
        public EnemyController()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.eventService.onPlayerPositionChanged.AddListener(SetPlayerPosition);
        }

        public virtual void PlayerEnteredRange()
        {
        }

        public void SetPlayerPosition(Vector2 playerPosition)
            => this.playerPosition = playerPosition;

        public virtual void FacePlayer()
        {
            Vector2 direction = playerPosition - (Vector2)GetEnemyTransform().position;

            if (direction.x > 0)
                GetEnemyTransform().localScale = new Vector3(-1, 1, 1);
            else if (direction.x < 0)
                GetEnemyTransform().localScale = new Vector3(1, 1, 1); 
        }

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

        public virtual void PlayerExitRanged()
        {
            
        }
        public abstract void TakeDamage(float damage);
    }
}
