using System;
using UI;
using UnityEngine;

namespace TimeSwitching
{
    public class TimeSwitchService
    {
        private TimeSwitchController timeSwitchController;
        
        public TimeSwitchService(TimeSwitchView timeSwitchView)
        {
            timeSwitchController = new TimeSwitchController(timeSwitchView);
        }

        public TimeSwitchController GetTimeSwitchController()
            => timeSwitchController;

       
    }
}
