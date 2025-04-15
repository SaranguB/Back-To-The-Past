using Enemy;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class DeadState<T> : IState<T> where T : EnemyController
    {
        private Animator enemyAnimator;

        public DeadState(Animator enemyAnimator)
        {
            this.enemyAnimator = enemyAnimator;
        }

        public T owner { get; set; }

        public void OnStateEnter()
        {

        }

        public void FixedUpdateState()
        {

        }
        public void UpdateState()
        {
        }

        public void OnStateExit()
        {
        }

    }
}
