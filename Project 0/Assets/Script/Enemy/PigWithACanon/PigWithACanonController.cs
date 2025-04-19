using Enemy;
using Main;
using StateMachine;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Wepons.Bomb;

namespace Enemy
{
    public class PigWithACanonController : EnemyController
    {
        private EnemyView enemyView;
        private PigWithACanonStateMachine pigWithACanonStateMachine;
        private bool isEnemyInPresent;
        public EnemySO enemyData;
        private BombPool bombPool;

        public bool WasInitiallyActive = true;
        private int pigHealth;

        public PigWithACanonController(EnemyView enemyView, BombPool bombPool)
        {
            SetEnemyView(enemyView);
            CreateStateMachine(bombPool);
            ChangeState(EnemyStates.Idle);
            SubscribeToEvents();

            InitializeValues(bombPool);
        }

        private void SetEnemyView(EnemyView enemyView)
        {
            this.enemyView = enemyView;
            this.enemyView.SetController(this);
            this.enemyView.enemyTriggerManager.SetEnemyTriggerZone(this);
            enemyData = this.enemyView.enemyData;
        }

        private void CreateStateMachine(BombPool bombPool)
        {
            pigWithACanonStateMachine = new PigWithACanonStateMachine(this, enemyView.enemyAnimator, bombPool);
        }

        private void InitializeValues(BombPool bombPool)
        {
            this.bombPool = bombPool;
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.AddListener(TimeSwitched);
        }

        public override void UnsubscribeToEvents()
        {
            GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.RemoveListener(TimeSwitched);
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



        public override void ChangeState(EnemyStates state)
            => pigWithACanonStateMachine.ChangeState(state);

        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
            ChangeState(EnemyStates.Attack);
        }

        public override void PlayerExitRanged()
        {
            base.PlayerExitRanged();
            ChangeState(EnemyStates.Idle);
        }

        public override bool IsInCastingState()
            => false;

        public IState<PigWithACanonController> GetCurrentState()
            => pigWithACanonStateMachine.GetCurrentState();

        public override void UpdateStateMachine()
            => pigWithACanonStateMachine.Update();

        public override void FixedUpdateStateMachine()
            => pigWithACanonStateMachine.FixedUpdate();

        public override void SetEnemyPosition(Vector2 pos)
        { }

        public override Vector2 GetPlayerPosition()
            => playerPosition;

        public override float GetSpeed()
            => enemyData.speed;

        public override float GetAttackRange()
            => enemyData.attackRange;

        public override Transform GetEnemyTransform()
            => enemyView.transform;

        public override float GetAttackDelay()
            => enemyData.attackDelay;

        public override EnemySO GetEnemyData()
                => enemyData;

        public override void TakeDamage(int damage)
        {

        }
        public override void FireBomb()
        {
            base.FireBomb();
             LightTheMatch();
        }

        private  void LightTheMatch()
        {
            enemyView.enemyAnimator.SetBool("IsAttacking", true);
        }

        public override void Fire()
        {

            if (enemyView.firePoint != null)
            {
                BombController bomb = bombPool.GetBomb();

                if (bomb != null)
                {
                    bomb.ConfigureBomb(enemyView.firePoint);
                    Vector2 direction = (playerPosition - (Vector2)enemyView.firePoint.position).normalized;

                    bomb.LaunchBomb(direction, enemyData.bombThrowForceX, enemyData.bombThrowForceY);

                    bomb.StartBombTimer();
                    enemyView.enemyAnimator.SetBool("IsAttacking", false);
                }
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
