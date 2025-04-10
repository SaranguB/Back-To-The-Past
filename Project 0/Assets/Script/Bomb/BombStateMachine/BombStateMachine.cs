using StateMachine;
using System;
using UnityEngine;
using Wepons.Bomb;

namespace Wepons.Bomb
{
    public class BombStateMachine : GenericStateMachine<BombController, BombState>
    {
        public BombStateMachine(BombController owner) : base(owner)
        {
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            AddState(BombState.Primed, new PrimedState());
            AddState(BombState.Exploding, new ExplodingState());
            AddState(BombState.Exploded, new ExplodedState());
        }
    }

    public enum BombState
    {
        Primed,
        Exploding,
        Exploded
    }
}
