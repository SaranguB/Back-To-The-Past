using Enemy;
using StateMachine;
using UnityEngine;

namespace Enemy
{
    public class catchingState<T> : IState<T> where T : EnemyController
    {
        public T owner { get; set; }

        public void OnStateEnter()
        {
            Debug.Log("catching");

        }

        public void UpdateState()
        {
            Debug.Log("Updating");
        }

        public void OnStateExit()
        {
        }

    }
}
