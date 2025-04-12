using Enemy;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class AttackState<T> : IState<T> where T : EnemyController
    {
        public T owner { get; set; }

        public void OnStateEnter()
        {

        }

        public void UpdateState()
        {
        }

        public void OnStateExit()
        {
        }

    }
}
