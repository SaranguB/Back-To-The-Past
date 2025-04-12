using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public class PigController : EnemyController
    {
        private EnemySO data;
        private EnemyView enemyView;
        private PigStateMachine pigStateMachine;
        public PigController(EnemyView enemyView)
        {
            this.enemyView = enemyView;
            enemyView.SetController(this);
            enemyView.enemyTriggerManager.SetEnemyTriggerZone(this);
            CreateStateMachine();
            pigStateMachine.ChangeState(PigStates.Idle);


        }

        private void CreateStateMachine()
        {
            pigStateMachine = new PigStateMachine(this, enemyView.enemyAnimator);
        }

        public override Coroutine SetCorotuine()
        {
            return enemyView.SetCorotuine();
        }

        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
            Debug.Log("changed1");
            pigStateMachine.ChangeState(PigStates.Catching);
        }

        public override bool isInCastingState()
        {
            return GetCurrentState() is catchingState<PigController>;
        }
        public IState<PigController> GetCurrentState()
            => pigStateMachine.GetCurrentState();

        public override void MoveTowardsPlayer()
        {
            pigStateMachine.Update();
        }
    }
}
