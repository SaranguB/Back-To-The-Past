
using StateMachine;
using Wepons.Bomb;

namespace Wepons.Bomb
{

    public class ExplodingState : IState<BombController>
    {
        public BombController owner { get; set; }

        public ExplodingState()
        {
            
           
        }

        public void OnStateEnter()
        {
            owner.SetAnimatorBool("IsExploded", true);
            owner.ChangeDamageAreaColliderState(true);
        }

        public void UpdateState()
        {
        }

        public void OnStateExit()
        {
        }


    }
}
