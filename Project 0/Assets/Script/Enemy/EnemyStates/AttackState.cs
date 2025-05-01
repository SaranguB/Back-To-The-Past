using StateMachine;
using UnityEngine;
using Wepons.Bomb;

namespace Enemy
{
    public class AttackState<T> : IState<T> where T : EnemyController
    {
        public T owner { get; set; }

        private Animator enemyAnimator;
        private float attackTimer = 0f;
        private float attackDelay;
        private BombPool bombPool;

        public AttackState(Animator enemyAnimator)
        {
            this.enemyAnimator = enemyAnimator;
        }

        public void OnStateEnter()
        {
            if (!(owner.GetPlayerState() == Player.PlayerState.Dead))
            {
                attackDelay = owner.GetAttackDelay();
                enemyAnimator.SetBool("IsAttacking", true);
                Attack();
            }
        }

        public void UpdateState()
        {
            if (!(owner.GetPlayerState() == Player.PlayerState.Dead))
            {
                AttackPlayerBetweenDelays();
                CheckPlayerAttackRange();
            }
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
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackDelay)
            {
                attackTimer = 0f;
                Attack();
            }
        }

        private void Attack()
        {
            if (enemyAnimator != null)
                enemyAnimator.SetTrigger("Attack");

            EnemySO enemyData = owner.GetEnemyData();

            switch (enemyData.enemyType)
            {
                case EnemyType.PigWithACanon:
                    owner.FireBomb();
                    break;
            }
        }

        public void OnStateExit()
        {
            if (enemyAnimator != null)
                enemyAnimator.SetBool("IsAttacking", false);
        }
    }
}
