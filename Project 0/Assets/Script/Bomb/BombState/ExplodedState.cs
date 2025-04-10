using StateMachine;
using UnityEngine;
using Wepons.Bomb;

namespace Wepons.Bomb
{

    public class ExplodedState : IState<BombController>
    {
        public BombController owner { get; set; }

        public void OnStateEnter()
        {
            owner.ChangeDamageAreaColliderState(false);
            owner.DisableBomb();
        }

        public void UpdateState()
        {
        }

        public void OnStateExit()
        {
        }


    }
}
