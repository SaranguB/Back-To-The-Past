using Enemy;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class IdleState<T> : IState<T> where T : EnemyController
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
             if (owner.IsEnemyViewActiveAndEnabled())
                enemyAnimator.SetBool("IsCatching", false);
        }

        public void UpdateState()
        {

        }
        public void FixedUpdateState()
        {

        }

        public void OnStateExit()
        {
        }

    }
}
