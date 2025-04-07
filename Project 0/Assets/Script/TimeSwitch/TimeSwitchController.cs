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

        private void SetController()
        {
            timeSwitchView.SetController(this);
        }
    }
}
