using StateMachine;
using UnityEngine;

namespace Player
{

    public class AliveState : IState<PlayerController>
    {
        public PlayerController owner { get; set; }

        public void OnStateEnter()
        {
        
        }

        public void UpdateState()
        {
        }

        public void FixedUpdateState()
        {

        }
        public void OnStateExit()
        {
        }


    }
}
