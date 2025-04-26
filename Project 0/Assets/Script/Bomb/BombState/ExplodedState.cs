using StateMachine;

namespace Wepons.Bomb
{
    public class ExplodedState : IState<BombController>
    {
        public BombController owner { get; set; }

        public void OnStateEnter()
        {
            owner.DisableBomb();
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
