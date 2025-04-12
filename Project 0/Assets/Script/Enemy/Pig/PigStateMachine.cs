using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public class PigStateMachine : GenericStateMachine<PigController, PigStates>
    {
        public PigStateMachine(PigController owner, Animator enemyAnimator) : base(owner)
        {
            CreateState(enemyAnimator);
            SetOwner();
        }

        private void CreateState(Animator enemyAnimator)
        {
            AddState(PigStates.Idle, new IdleState<PigController>(enemyAnimator));
            AddState(PigStates.Catching, new catchingState<PigController>());
            AddState(PigStates.Attack, new AttackState<PigController>());
            AddState(PigStates.Dead, new DeadState<PigController>());
        }
    }

    public enum PigStates
    {
        Idle,
        Catching,
        Attack,
        Dead,
    }
}
