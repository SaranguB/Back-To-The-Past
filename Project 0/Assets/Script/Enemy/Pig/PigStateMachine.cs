using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public class PigStateMachine : GenericStateMachine<PigController, EnemyStates>
    {
        public PigStateMachine(PigController owner, Animator enemyAnimator) : base(owner)
        {
            CreateState(enemyAnimator);
            SetOwner();
        }

        private void CreateState(Animator enemyAnimator)
        {
            AddState(EnemyStates.Idle, new IdleState<PigController>(enemyAnimator));
            AddState(EnemyStates.Catching, new catchingState<PigController>(enemyAnimator));
            AddState(EnemyStates.Attack, new AttackState<PigController>(enemyAnimator));
        }
    }
}
