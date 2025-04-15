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
            enemyController.UpdateStateMachine();

        }
        private void FixedUpdate()
        {
            enemyController.FixedUpdateStateMachine();
        }
    }
}
