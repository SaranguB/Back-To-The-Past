
using StateMachine;

namespace Wepons.Bomb
{
    public class ExplodingState : IState<BombController>
    {
        public BombController owner { get; set; }

        public void OnStateEnter()
        {
            owner.PlayBombExposionSound();
            owner.SetAnimatorBool("IsExploded", true);
            owner.ChangeDamageAreaColliderState(true);
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
