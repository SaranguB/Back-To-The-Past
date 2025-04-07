using StateMachine;
using System;
using UnityEngine;

namespace Time
{
    public class TimeStateMachine : GenericStateMachine<TimeSwitchController, TimeState>
    {
        public TimeStateMachine(TimeSwitchController owner) : base(owner) 
        {
            this.owner = owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            AddState(TimeState.Past, new )
        }
    }


    public enum TimeState
    {
        Past,
        Present
    }
}
