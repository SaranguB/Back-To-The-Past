using Main;
using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public class PigController : EnemyController
    {
        public EnemySO enemyData;
        private EnemyView enemyView;
        private PigStateMachine pigStateMachine;

        private bool isEnemyInPresent;
        public bool WasInitiallyActive = true;
        private int pigHealth;

        public PigController(EnemyView enemyView)
        {
            SetEnemyView(enemyView);

            CreateStateMachine();
            ChangeState(EnemyStates.Idle);
            SubscribeToEvents();

            InitializeValues();
        }

        private void InitializeValues()
        {
            pigHealth = enemyData.health;
        }

        public void SubscribeToEvents()
        {
            GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.AddListener(TimeSwitched);
        }

        public override void UnsubscribeToEvents()
        {
            GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.RemoveListener(TimeSwitched);
        }

        private void SetEnemyView(EnemyView enemyView)
        {
            this.enemyView = enemyView;
            this.enemyView.SetController(this);
            this.enemyView.enemyTriggerManager.SetEnemyTriggerZone(this);
            enemyData = this.enemyView.enemyData;
        }

        private void CreateStateMachine()
        {
            pigStateMachine = new PigStateMachine(this, enemyView.enemyAnimator);
        }

        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
            ChangeState(EnemyStates.Catching);
        }

        public override void PlayerExitRanged()
        {
            base.PlayerExitRanged();
            ChangeState(EnemyStates.Idle);
        }
        public override bool IsInCastingState()
        {
            return GetCurrentState() is catchingState<PigController>;
        }
        public IState<PigController> GetCurrentState()
            => pigStateMachine.GetCurrentState();

        public override void UpdateStateMachine()
            => pigStateMachine.Update();

        public override void FixedUpdateStateMachine()
            => pigStateMachine.FixedUpdate();

        public override void SetEnemyPosition(Vector2 pos)
            => enemyView.transform.position = pos;

        public override Vector2 GetPlayerPosition()
            => playerPosition;

        public override float GetSpeed()
            => enemyData.speed;

        public override float GetAttackRange()
            => enemyData.attackRange;

        public override void ChangeState(EnemyStates state)
             => pigStateMachine.ChangeState(state);

        public override Transform GetEnemyTransform()
            => enemyView.transform;

        public override float GetAttackDelay()
            => enemyData.attackDelay;

        public override EnemySO GetEnemyData()
                => enemyData;

        public override void TakeDamage(int damage)
        {
            pigHealth -= damage;

            if (pigHealth <= 0)
                enemyView.EnemyIsDead();
        }

        private void TimeSwitched(bool value)
        {
            isEnemyInPresent = value;

            if (isEnemyInPresent)
            {
                enemyView.TimeSwitchedToPresent();
            }
            else
            {
                enemyView.TimeSwitchedToPast();
            }
        }

        public override bool IsEnemyViewActiveAndEnabled()
        {
            if (enemyView != null)
                return enemyView.IsEnemyViewActiveAndEnabled();

            return false;
        }
    }
}
