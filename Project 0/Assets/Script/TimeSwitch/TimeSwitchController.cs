using System;
using UnityEngine;

namespace Time
{
    public class TimeSwitchController
    {
        private TimeSwitchView timeSwitchView;
        private TimeSwitchModel timeSwitchModel;
        public TimeSwitchController(TimeSwitchView timeSwitchView)
        {
            this.timeSwitchView = timeSwitchView;
            timeSwitchModel = new TimeSwitchModel();
            SetController();
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
