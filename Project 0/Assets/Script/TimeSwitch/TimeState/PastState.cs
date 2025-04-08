using StateMachine;
using TimeSwitching;
using UnityEngine;

namespace TimeSwitching
{
    public class PastState : IState<TimeSwitchController>
    {

        public TimeSwitchController owner { get; set; }
        public TimeAffectedObjectController[] objectController;

        public PastState(TimeAffectedObjectController[] objectController)
        {
            this.objectController = objectController;
        }


        public void OnStateEnter()
        {
            foreach (TimeAffectedObjectController affectedObject in objectController)
            {
               owner.SetPastProperties(affectedObject);
            }
        }

        public void UpdateState()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
