using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
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
            if (enemyController.isInCastingState())
            {
                enemyController.MoveTowardsPlayer();
            }
        }
        public Coroutine SetCorotuine()
        {
            return StartCoroutine(CheckPlayerDistance());
        }

        private IEnumerator CheckPlayerDistance()
        {
            yield return null;
        }
    }
}
