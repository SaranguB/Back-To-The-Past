using StateMachine;
using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyController
    {
        private EnemyViewCollection enemyViewCollection;

        public EnemyController()
        {

        }

        public virtual Coroutine SetCorotuine()
        {
            return null;
        }

        public virtual void PlayerEnteredRange()
        {
        }

        public virtual bool isInCastingState()
        {
            return false;
        }
        
        public virtual void MoveTowardsPlayer()
        {
            
        }
    }
}
