using StateMachine;
using System;
using UnityEngine;

namespace Time
{
    public class TimeStateMachine : GenericStateMachine<TimeSwitchController, TimeState>
    {
        private TimeAffectedObjectController[] objectController;
        public TimeStateMachine(TimeSwitchController owner, TimeAffectedObjectController[] timeAffectedObjectControllers) : base(owner) 
        {
            objectController = timeAffectedObjectControllers;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            AddState(TimeState.Past, new PastState(objectController));
            AddState(TimeState.Present, new PresentState(objectController));
        }
    }


    public enum TimeState
    {
        Past,
        Present
    }
}
