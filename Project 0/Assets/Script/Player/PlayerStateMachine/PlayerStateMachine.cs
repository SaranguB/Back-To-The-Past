using StateMachine;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : GenericStateMachine<PlayerController, PlayerState>
    {

        public PlayerStateMachine(PlayerController owner) : base(owner)
        {
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            AddState(PlayerState.Alive, new AliveState());
            AddState(PlayerState.Dead, new DeadState());
        }
    }
    public enum PlayerState
    {
        Alive,
        Dead
    }
}
