using Enemy;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class catchingState<T> : IState<T> where T : EnemyController
    {
        private Animator enemyAnimator;
        public T owner { get; set; }

        public catchingState(Animator enemyAnimator)
        {
            this.enemyAnimator = enemyAnimator;
        }


        public void OnStateEnter()
        {
            enemyAnimator.SetBool("IsCatching", true);
        }

        public void UpdateState()
        {
            owner.FacePlayer();
        }

        public void FixedUpdateState()
        {
            MoveTowardsPlayer();
        }

        private void MoveTowardsPlayer()
        {
            Vector2 enemyPos = owner.GetEnemyTransform().position;
            Vector2 playerPos = owner.GetPlayerPosition();
            float speed = owner.GetSpeed();
            float attackRange = owner.GetAttackRange();

            float distance = Vector2.Distance(enemyPos, playerPos);

            if (distance > attackRange)
            {

                Vector2 newPos = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
                owner.SetEnemyPosition(newPos);
            }
            else
            {
                owner.ChangeState(EnemyStates.Attack);
            }
        }

        public void OnStateExit()
        {
        }

    }
}
