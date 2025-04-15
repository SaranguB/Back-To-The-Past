using Player;
using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyTriggerManager : MonoBehaviour
    {
        public EnemyController enemy;
        public void SetEnemyTriggerZone(EnemyController enemyController)
        {
           this.enemy = enemyController;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerView>() != null)
            {
                enemy.PlayerEnteredRange();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {

            if (other.gameObject.GetComponent<PlayerView>() != null)
            {
                enemy.PlayerExitRanged();
            }
        }
    }
}
