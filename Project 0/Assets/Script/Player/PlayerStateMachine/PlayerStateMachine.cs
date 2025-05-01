using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : GenericStateMachine<PlayerController, PlayerState>
    {
        public PlayerStateMachine(PlayerController owner, Animator playerAnimator) : base(owner)
        {
            CreateStates(playerAnimator);
            SetOwner();
        }

        private void CreateStates(Animator playerAnimator)
        {
            AddState(PlayerState.Idle, new IdleState(playerAnimator));
            AddState(PlayerState.Running, new RunningState(playerAnimator));
            AddState(PlayerState.Jumping, new JumpingState(playerAnimator));
            AddState(PlayerState.Attack, new AttackingState(playerAnimator));
            AddState(PlayerState.Falling, new FallingState(playerAnimator));
            AddState(PlayerState.Dashing, new DashingState(playerAnimator));
            AddState(PlayerState.TimeSwitching, new TimeSwitchingState(playerAnimator));
            AddState(PlayerState.Dead, new DeadState(playerAnimator));
        }
    }

    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Dashing,
        TimeSwitching,
        Attack,
        Dead,
    }
}
