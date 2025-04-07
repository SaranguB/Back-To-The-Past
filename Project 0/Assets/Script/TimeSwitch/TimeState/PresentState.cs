using StateMachine;
using Time;
using UnityEngine;

namespace Time
{
    public class PresentState : IState<TimeSwitchController>
    {

        public TimeSwitchController owner { get; set; }
        public TimeAffectedObjectController[] objectController;

        public PresentState(TimeAffectedObjectController[] objectController)
        {
            this.objectController = objectController;
        }


        public void OnStateEnter()
        {
            foreach (TimeAffectedObjectController affectedObject in objectController)
            {
                owner.SetPresentproperties(affectedObject);
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
