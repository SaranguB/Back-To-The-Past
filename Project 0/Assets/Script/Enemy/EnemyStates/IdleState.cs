using Enemy;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class IdleState<T> : IState<T> where T :EnemyController
    {
        public T owner { get; set; }

        private Animator enemyAnimator;
        private Coroutine playerCheckCorourine;
        public IdleState(Animator enemyAnimator)
        {
            this.enemyAnimator = enemyAnimator;
        }

        public void OnStateEnter()
        {
            Debug.Log("idle");
        }

        public void UpdateState()
        {
            
        }

        public void OnStateExit()
        {
        }

    }
}
