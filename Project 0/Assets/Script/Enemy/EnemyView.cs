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
        public bool WasActiveInitially = true;

        [Header("EnemyWithAWepon")]
        public Transform firePoint;

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

        public void TakeDamage(int damage)
        {
            enemyController.TakeDamage(damage);
        }

        public bool IsEnemyViewActiveAndEnabled()
        {
            return this.isActiveAndEnabled;
        }

        public void TimeSwitchedToPresent()
        {
            
                if (WasActiveInitially)
                {
                    if (enemyController.GetEnemyData().inPresent)
                    {
                        this.gameObject.SetActive(true);
                    }
                    else
                    {
                        this.gameObject.SetActive(false);
                    }
                }
        }

        public void TimeSwitchedToPast()
        {
            if (WasActiveInitially)
            {
                if (enemyController.GetEnemyData().inPast)
                {
                    this.gameObject.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        public void SetOriginallyActive()
        {
            WasActiveInitially = true;
        }

        public void EnemyIsDead()
        {
            enemyController.UnsubscribeToEvents();
            Destroy(this.gameObject);

        }

        public void FireCanon()
        {
            enemyController.Fire();
        }
    }
}
