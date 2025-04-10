using StateMachine;

namespace Player
{ 
    public class DeadState : IState<PlayerController>
    {
        public PlayerController owner { get; set; }

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
