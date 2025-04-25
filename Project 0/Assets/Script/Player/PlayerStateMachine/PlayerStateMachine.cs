using StateMachine;
using System;
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
            AddState(PlayerState.Alive, new AliveState(playerAnimator));
            AddState(PlayerState.Dead, new DeadState(playerAnimator));
            AddState(PlayerState.Hurt, new HurtState(playerAnimator));
        }
    }
    public enum PlayerState
    {
        Alive,
        Dead,
        Hurt
    }
}
