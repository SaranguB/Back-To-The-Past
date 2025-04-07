using System;
using UnityEngine;

namespace Time
{
    public class TimeSwitchController
    {
        private TimeSwitchView timeSwitchView;
        private TimeSwitchModel timeSwitchModel;
        private TimeStateMachine timeStateMachine;
        public TimeSwitchController(TimeSwitchView timeSwitchView)
        {
            this.timeSwitchView = timeSwitchView;
            timeSwitchModel = new TimeSwitchModel();
            SetController();
            CreateStateMachine();

            timeStateMachine.ChangeState(TimeState.Present);
        }

        private void CreateStateMachine()
        {
            timeStateMachine = new TimeStateMachine(this, timeSwitchView.GetAffectedObjects());
        }

        public void SetPastProperties(TimeAffectedObjectController affectedObject)
        {
            affectedObject.SetPastProperties();
        }

        public void SetPresentproperties(TimeAffectedObjectController affectedObject)
        {
            affectedObject.SetPresentProperties();
        }



        private void SetController()
        {
            timeSwitchView.SetController(this);
        }
    }
}
