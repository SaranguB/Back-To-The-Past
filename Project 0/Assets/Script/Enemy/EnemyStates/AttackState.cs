using Enemy;
using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public class AttackState<T> : IState<T> where T : EnemyController
    {
        public T owner { get; set; }

        private Animator enemyAnimator;
        private float attackTimer = 0f;
        private float attackDelay;

        public AttackState(Animator enemyAnimator)
        {
            this.enemyAnimator = enemyAnimator;
        }

        public void OnStateEnter()
        {
            attackDelay = owner.GetAttackDelay();
            enemyAnimator.SetBool("IsAttacking", true);
            Attack();
        }

        public void UpdateState()
        {
            AttackPlayerBetweenDelays();
            CheckPlayerAttackRange();
        }

        public void FixedUpdateState()
        {

        }


        private void CheckPlayerAttackRange()
        {
            float distance = Vector2.Distance(owner.GetPlayerPosition(), owner.GetEnemyTransform().position);

            if (distance > owner.GetAttackRange())
            {
                owner.ChangeState(EnemyStates.Catching);
            }
        }

        private void AttackPlayerBetweenDelays()
        {
            Debug.Log("Attack");
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackDelay)
            {
                attackTimer = 0f;
                Attack();
            }
        }

        private void Attack()
        {
            enemyAnimator.SetTrigger("Attack");
        }

        public void OnStateExit()
        {
            enemyAnimator.SetBool("IsAttacking", false);
        }

    }
}
