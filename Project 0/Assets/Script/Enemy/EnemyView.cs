using System;
using System.Collections;
using UnityEngine;
using Wepons.Bomb;

namespace Enemy
{
    public class EnemyView : MonoBehaviour, IDamagableFromBomb
    {
        private EnemyController enemyController;

        public EnemySO enemyData;
        public Animator enemyAnimator;
        public EnemyTriggerManager enemyTriggerManager;
        public void SetController(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        private void Update()
        {
            enemyController.UpdateStateMachine();

        }
        private void FixedUpdate()
        {
            enemyController.FixedUpdateStateMachine();
        }

        public void TakeDamageFromBomb(float damage)
        {
            enemyController.TakeDamage(damage);
        }

        public bool IsEnemyViewActiveAndEnabled()
        {
            return this.isActiveAndEnabled;
        }
    }
}
