using StateMachine;
using UnityEngine;
using Wepons.Bomb;

namespace Wepons.Bomb
{

    public class PrimedState : IState<BombController>
    {
        public BombController owner { get; set; }

        public PrimedState()
        {

            
        }

        public void OnStateEnter()
        {
            owner.StartBombTimer();
        }

        public void UpdateState()
        {
        }

        public void OnStateExit()
        {
        }


    }
}
